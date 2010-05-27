/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package Agent;

/**
 *
 * @author Patrick
 */
public class Credentials
{
    public static boolean CanLogin(String userName, String passWord)
    {
        if(userName.equalsIgnoreCase("paddy") && passWord.equalsIgnoreCase("paddy"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
