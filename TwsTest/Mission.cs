using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TwsTest
{
    public class Mission
    {
        #region Constructors
        public Mission() { }
        public Mission(string parameters)
        {
            Parameters = parameters;
        }
        #endregion Constructors

        #region Members
        private Plateau _plateau = null;
        private List<Rover> _rovers = null;
        private Regex _rxDirection = new Regex(@"^[NESW]$");
        private Regex _rxRoute = new Regex(@"^[LRM]+$");
        #endregion Members

        #region Properties
        public string Parameters { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
        #endregion Properties

        #region Methods
        #region Public Methods
        public bool RunMission(string parameters)
        {
            Parameters = parameters;
            return RunMission();
        }

        public bool RunMission()
        {
            Message = "";
            Result = "";
            try
            {
                // Validate the mission parameters
                if (string.IsNullOrWhiteSpace(Parameters))
                {
                    Message = "No mission parameters supplied";
                    return false;
                }

                var lines = Parameters.SplitLines();
                if (lines.Length < 3)
                {
                    Message = "Invalid mission parameters supplied - Not enough lines";
                    return false;
                }
                if (lines.Length % 2 != 1 && !(lines.Length % 2 == 0 && string.IsNullOrWhiteSpace(lines[lines.Length - 1])))
                {
                    Message = "Invalid mission parameters supplied - Wrong number of lines.  Needs to be a size followed by pairs of rover details";
                    return false;
                }

                var maxXy = lines[0].Split();
                if (maxXy.Length != 2)
                {
                    Message = "Invalid mission parameters supplied - Plateau size invalid";
                    return false;
                }
                if (!maxXy[0].IsInteger(false))
                {
                    Message = "Invalid mission parameters supplied - Plateau maximum X is not a positive integer";
                    return false;
                }
                if (!maxXy[1].IsInteger(false))
                {
                    Message = "Invalid mission parameters supplied - Plateau maximum Y is not a positive integer";
                    return false;
                }
                var maxX = 0;
                var maxY = 0;
                if (!int.TryParse(maxXy[0], out maxX))
                {
                    Message = "Invalid mission parameters supplied - maximum X is invalid";
                    return false;
                }
                if (!int.TryParse(maxXy[1], out maxY))
                {
                    Message = "Invalid mission parameters supplied - maximum Y is invalid";
                    return false;
                }
                var pSize = new Coordinates(maxX, maxY);

                var l = 1;
                while (l < lines.Length)
                {
                    if (lines.Length % 2 == 0 && l == lines.Length - 1) // Is this a last blank line
                    {
                        break;
                    }
                    var start = lines[l].Split();
                    if (start.Length != 3)
                    {
                        Message = "Invalid mission parameters supplied - Rover start";
                        return false;
                    }
                    if (!start[0].IsInteger(false))
                    {
                        Message = "Invalid mission parameters supplied - Rover start X is not a positive integer";
                        return false;
                    }
                    if (!start[1].IsInteger(false))
                    {
                        Message = "Invalid mission parameters supplied - Rover start Y is not a positive integer";
                        return false;
                    }

                    if (!_rxDirection.IsMatch(start[2]))
                    {
                        Message = "Invalid mission parameters supplied - Rover start direction is invalid";
                        return false;
                    }

                    if (!_rxRoute.IsMatch(lines[l + 1]))
                    {
                        Message = "Invalid mission parameters supplied - Rover route is invalid";
                        return false;
                    }

                    l += 2;
                }

                // Build the plateau
                _plateau = new Plateau(pSize);

                _rovers = new List<Rover>();

                // Run each rover in turn
                l = 1;
                while (l < lines.Length)
                {
                    if (lines.Length % 2 == 0 && l == lines.Length - 1) // Is this a last blank line
                    {
                        break;
                    }
                    var start = lines[l].Split();
                    var x = 0;
                    var y = 0;
                    if (!int.TryParse(start[0], out x)) // In theory the validation should've checked this already
                    {
                        Message = "Invalid mission parameters supplied - Rover start X is invalid";
                        return false;
                    }
                    if (!int.TryParse(start[1], out y)) // In theory the validation should've checked this already
                    {
                        Message = "Invalid mission parameters supplied - Rover start Y is invalid";
                        return false;
                    }

                    // Create the new rover
                    var rover = new Rover(x, y, start[2]);
                    // Did the rover land on the plateau?
                    if (!_plateau.IsInsidePlateau(rover.Position))
                    {
                        Message = "Mission failed - Rover landed outside of the plateau";
                        return false;
                    }

                    if (_rovers.Count > 0)
                    {
                        // Check we're not landing on an existing rover
                        foreach (var existingRover in _rovers)
                        {
                            if (rover.Position.Matches(existingRover.Position))
                            {
                                Message = "Mission failed - Rover landed on another rover";
                                return false;
                            }
                        }
                    }
                    _rovers.Add(rover);
                    _plateau.Map(rover);

                    // Process the rover's mapping instructions
                    foreach (var c in lines[l + 1].ToCharArray())
                    {
                        switch (c)
                        {
                            case 'L':
                                rover.TurnLeft();
                                break;
                            case 'R':
                                rover.TurnRight();
                                break;
                            case 'M':
                                var nextPos = rover.WillMoveForwardTo();
                                if (!_plateau.IsInsidePlateau(nextPos))
                                {
                                    Message = "Mission failed - Rover fallen off the plateau";
                                    return false;
                                }
                                if (_rovers.Count > 0)
                                {
                                    // Check we're not landing on an existing rover
                                    if (_rovers.Any(existingRover => !rover.Equals(existingRover) && rover.Position.Matches(existingRover.Position)))
                                    {
                                        Message = "Mission failed - Rover crashed into another rover";
                                        return false;
                                    }
                                }
                                // We've checked for problems so move the rover
                                rover.MoveForward();
                                _plateau.Map(rover);
                                break;
                        }
                    }
                    l += 2;
                }

                // Set up the return string
                var sbRet = new StringBuilder(50);
                foreach (var rover in _rovers)
                {
                    sbRet.AppendLine(rover.Position.X + " " + rover.Position.Y + " " + rover.Direction.ToStr());
                }
                Result = sbRet.ToString().TrimEndNewLine();
                Message = "Successful Mission";

                return true;
            }
            catch (Exception e)
            {
                // And overriding catch to make sure we didn't miss any validation (or other) issues
                Message = "Unknown error: " + e.Message;
                return false;
            }
        }
        #endregion Public Methods

        #region Private Methods
        #endregion Private Methods
        #endregion Methods
    }
}
