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

namespace SeaBattle.DataBase
{

    public enum DataBaseExceptionType { WrongDBContext }

    public class DataBaseException : Exception
    {
        private DataBaseExceptionType _type;
        private string _message;

        public DataBaseExceptionType Type
        {
            get { return _type; }
        }

        public override string Message
        {
            get
            {
                return _message;
            }
        }

        public DataBaseException(DataBaseExceptionType type, string message)
        {
            _type = type;
            _message = message;
        }
    }
}
