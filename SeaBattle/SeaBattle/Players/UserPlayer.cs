using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SeaBattle.Logic;

namespace SeaBattle.Players
{
    public class UserPlayer : Player
    {
        public override bool Shoot(IntPoint point, Map targetsMap)
        {
            int unitIndex = targetsMap.Fields[point.Y, point.X].IndexOfUnit;
            targetsMap.Fields[point.Y, point.X].IsDiscoverd = true;

            if (unitIndex == -1)
            {
                return false;
            }

            else
            {
                targetsMap.units[unitIndex].UpdateUnitState(point);

                if (targetsMap.units[unitIndex].IsDestroyed())
                {
                    MessageBox.Show("Zniszczyłes jednostke wroga");
                }

                return true;
            }
        } 
    }
}
