using System;
using System.Data;
using System.Linq;
using System.Web;
public static class Global
{
    static bool _easyBool = false;
    static bool _normBool = false;
    static bool _hardBool = false;

    static bool _gnlBool = false;
    static bool _prgBool = false;
    static bool _vgmBool = false;

    public static bool EasyBool
    {
        get {   return _easyBool;   }
        set {   _easyBool = value;  }
    }

    public static bool NormBool
    {
        get { return _normBool; }
        set { _normBool = value; }
    }

    public static bool HardBool
    {
        get { return _hardBool; }
        set { _hardBool = value; }
    }

    public static bool GnlBool
    {
        get { return _gnlBool; }
        set { _gnlBool = value; }
    }

    public static bool PrgBool
    {
        get { return _prgBool; }
        set { _prgBool = value; }
    }

    public static bool VgmBool
    {
        get { return _vgmBool; }
        set { _vgmBool = value; }
    }

}