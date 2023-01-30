using System;
using System.ComponentModel.Design;
using System.Windows.Controls;
using Team3;
using Team3DesktopApp.Model;

namespace Team3DesktopApp.ViewModel;

public class LoginViewModel
{

    public bool Login(string userName, string password)
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        {
            return false;
        }
        // send to dal
        return true;

    }

}