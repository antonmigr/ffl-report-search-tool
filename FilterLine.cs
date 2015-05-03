using System;

public class FilterLine
{
    
    static int xMargin = 5; 
    static int yMargin = 5; 
    Boolean open = false;
    ComboBox field = null;
    TextBox sqlFilter = null;
    ComboBox valueCombo = null; 
    DateTimePicker valueDate = null;
    TextBox textValue = null;
    Boolean Valid = false;
    String Name = ""; 

    public FilterLine(String nm, bool o )
	{
        this.Name = nm; 
        this.field = null;
        this.sqlFilter = null;
        this.valueCombo = null;
        this.valueDate = null;
        this.textValue = null;
        this.Valid = false;
        setOpen(o);
	}
    
    public void setOpen(bool set) 
    {
        this.open=set; 
        if ( this.open ) 
        {
            DisplayOne(); 
        }
    }
    private void DisplayOne()
    {
        this.field = new ComboBox(this.Name+"FieldComboBox");
       
    }
}
