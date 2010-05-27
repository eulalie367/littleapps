
package Forms;
    //<editor-fold defaultstate="collapsed" desc=" History ">
/**
 @author Patrick
 Feb 4, 2009
 */
    //</editor-fold>

    //<editor-fold defaultstate="collapsed" desc=" Imports ">
import javax.microedition.lcdui.*;
import javax.microedition.midlet.*;
import Audio.*;
    //</editor-fold>
public class Main extends Form implements CommandListener
{
    //<editor-fold defaultstate="collapsed" desc=" Constructors ">
    public Main()
    {
        super("Main");
        this.setEvents();
    }
    /** Loads and Displays */
    public Main(MIDlet Parent)
    {
        super("Main");
        parent = Parent;
        this.lastDisplay = display().getCurrent();
        this.setEvents();
        this.Display(parent);
    }
    //</editor-fold>

    //<editor-fold defaultstate="collapsed" desc=" Properties ">
    public MIDlet Parent() { return parent; }
    public Displayable LastDisplay() { return lastDisplay; }
    //</editor-fold>

    //<editor-fold defaultstate="collapsed" desc=" Private variables ">
    private MIDlet parent;
    private Displayable lastDisplay;
    private Display display()
    {
        if(this.parent != null)
            return Display.getDisplay(this.parent);
        else
            return null;
    }
//</editor-fold>

    //<editor-fold defaultstate="collapsed" desc=" Event Handling ">
    Command confirm;
    Command exit;
    private void setEvents()
    {
        this.confirm = new Command("Main", Command.OK, 0);
        if(this.lastDisplay != null)
            this.exit = new Command("Back", Command.EXIT, 0);
        else
            this.exit = new Command("Exit", Command.EXIT, 0);

        this.addCommand(this.confirm);
        this.addCommand(this.exit);

        this.setCommandListener(this);
    }
    public void commandAction(Command command, Displayable displayable)
    {
        switch(command.getCommandType())
        {
            case Command.OK :
                this.OK(displayable);
            break;
            case Command.EXIT :
                this.destroyForm(true);
            break;
            //default:
        }
    }
    //</editor-fold>

    //<editor-fold defaultstate="collapsed" desc=" Public Methods ">
    public void destroyForm(boolean unconditional)
    {
        //Show Farewell Screen
        if(this.lastDisplay != null)
        {
            display().setCurrent(lastDisplay);
        }
        else
        {
            parent.notifyDestroyed();
            parent = null;
        }
    }
    public void OK(Displayable displayable)
    {
        if(1==1)
        {
            display().setCurrent(new RecordForm());
        }
    }
    public void Display(MIDlet mIDlet)
    {
        this.parent = mIDlet;
        display().setCurrent(this);
    }
    //</editor-fold>

    //<editor-fold defaultstate="collapsed" desc=" Private Methods ">
    //</editor-fold>
}
