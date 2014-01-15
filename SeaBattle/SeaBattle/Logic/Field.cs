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

namespace SeaBattle.Logic
{

    public enum FieldType { Water, Land }


    public class Field
    {
        #region variables

        FieldType type;
        int indexOfUnit;
        bool isDiscoverd;

        #endregion

        #region properties

        public FieldType Type
        {
            get { return type; }
        }

        public bool IsDiscoverd
        {
            get { return isDiscoverd; }
            set { isDiscoverd = value; }
        }

        public bool HaveUnit
        {
            get 
            {
                if (indexOfUnit == -1)
                    return false;
                else
                    return true;
            }
        }

        public int IndexOfUnit
        {
            get { return indexOfUnit; }
            set { indexOfUnit = value; }
        }

        #endregion

        #region constructors

        public Field(FieldType _type)
        {
            type = _type;
            isDiscoverd = false;
            indexOfUnit = -1;
        }

        #endregion
    }
}
