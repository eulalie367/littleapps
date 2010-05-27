/*
    Create "Form" Midlets and use this as the calling page
 */

import javax.microedition.lcdui.*;
import javax.microedition.midlet.*;
import Forms.*;
import Audio.*;

/**
 * @author Patrick
 */
public class Otto extends MIDlet
{
    Home home;
    public Display getDisplay () { return Display.getDisplay(this); }

    public void startApp()
    {
        home = new Home(this);
    }

    public void pauseApp()
    {
        //kill connections and such
    }

    public void destroyApp(boolean unconditional)
    {
        home = null;
    }
}

