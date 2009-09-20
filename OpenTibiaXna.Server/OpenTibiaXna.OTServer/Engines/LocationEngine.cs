using System;
using System.Collections.Generic;

namespace OpenTibiaXna.OTServer.Engines
{
    public class LocationEngine
    {
        public int X;
        public int Y;
        public int Z;

        public LocationEngine(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return X + ", " + Y + ", " + Z;
        }

        public LocationEngine Offset(Direction direction)
        {
            int x = X, y = Y, z = Z;

            switch (direction)
            {
                case Direction.North:
                    y--;
                    break;
                case Direction.South:
                    y++;
                    break;
                case Direction.West:
                    x--;
                    break;
                case Direction.East:
                    x++;
                    break;
                case Direction.NorthWest:
                    x--;
                    y--;
                    break;
                case Direction.SouthWest:
                    x--;
                    y++;
                    break;
                case Direction.NorthEast:
                    x++;
                    y--;
                    break;
                case Direction.SouthEast:
                    x++;
                    y++;
                    break;
            }

            return new LocationEngine(x, y, z);
        }

        public bool CanSee(LocationEngine loc)
        {
	        if(Z <= 7){
		        //we are on ground level or above (7 -> 0)
		        //view is from 7 -> 0
		        if(loc.Z > 7){
			        return false;
		        }
	        }
	        else if(Z >= 8){
		        //we are underground (8 -> 15)
		        //view is +/- 2 from the floor we stand on
		        if(Math.Abs(Z - loc.Z) > 2){
			        return false;
		        }
	        }

	        //negative offset means that the action taken place is on a lower floor than ourself
	        int offsetz = Z - loc.Z;

            if ((loc.X >= X - 8 + offsetz) && (loc.X <= X + 9 + offsetz) &&
                (loc.Y >= Y - 6 + offsetz) && (loc.Y <= Y + 7 + offsetz))
		        return true;

	        return false;
        }

        public bool IsInRange(LocationEngine second, bool sameFloor, double range)
        {
            return IsInRange(this, second, sameFloor, range);
        }

        public static bool IsInRange(LocationEngine first, LocationEngine second, bool sameFloor, double range)
        {
            if (sameFloor && first.Z != second.Z) return false;
            int dx = first.X - second.X;
            int dy = first.Y - second.Y;
            return Math.Sqrt(Math.Pow(dx, 2.0) + Math.Pow(dy, 2.0)) <= range;
        }
    }
}