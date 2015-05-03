//This code is for the demo version of the Lab Report Search Tool.
//Some features that are part of the final release version have been ommitted from 
//this version.  Parts of the code that are intended for the production version
//that is used at Forensic Fluids have been commented out.  This version may contain
//bugs but it demonstrates the main features of the release version that is used by
//FFL employees every day.
//
//Developed by Barry DeYoung, Anton Aleynikov and Alan Douglas.

using PresentationControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;
using itextsharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Principal;
using System.Security.AccessControl;
using System.Net.Mail;
using MySql.Data.MySqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicQuery
{
    [TestClass]
    public partial class QueryBuilder : Form
    {
        public int addCounter = 0;
        private static String[,] FieldsArray = new String[19,9];
        private static String[,] FilterArray = new String[21,3];
        
        private int FieldsArrayCount = 0;
        private int FilterArrayCount = 0;

        //number of combo box rows in step 2
        private static int filterCount = 6;

        //arrays: Field = column type, Filter = filter conditional, Value = actual user's value of the filter
        private ComboBox[] Field = new ComboBox[filterCount];
        private ComboBox[] Filter = new ComboBox[filterCount];
        private CheckBoxComboBox[] Value = new CheckBoxComboBox[filterCount];

        //array of red X/green checkmark images to be placed next to the value combo box
        private PictureBox[] valid = new PictureBox[filterCount];

        private bool ENCRYPT_EMAIL = false;
  

        public QueryBuilder()
        {
            //initialize windows forms UI elements
            InitializeComponent();

            //initialize the step 2 filter combo boxes
            InitializeFilter(); 
            DrawFilterLines();


            //put all fields and their properties into the large fields array
            insertintoFieldsArray("Customer", "Customer ID", "Number", "String", "75", "customer", "1", "FactSamples", "CustomerID");
            insertintoFieldsArray("Customer", "Customer Name ", "String", "String", "225", "customer", "1", "FactSamples", "customername");
            insertintoFieldsArray("Customer", "State", "String", "List-SELECT Distinct state from FactSamples where shippingstate is not null order by shippingstate;", "45", "customer", "0", "FactSamples", "State");
            insertintoFieldsArray("Customer", "County", "String", "String", "135", "customer", "0", "FactSamples", "County");
            insertintoFieldsArray("Customer", "Primary Market", "String", "List-Select distinct primarymarket from FactSamples order by primarymarket ; ", "100", "customer", "0", "FactSamples ", "primarymarket ");
            insertintoFieldsArray("Customer", "Sales Rep", "String", "List-select distinct SalesRep from FactSamples where salesrepid is not null order by SalesRepID  ; ", "50", "customer", "0", "FactSamples", "SalesRep");
            insertintoFieldsArray("Customer", "Parent LIMS ID", "String", "String", "85", "customer", "0", "FactSamples", "parentlims");
            insertintoFieldsArray("Samples", "Lab ID", "Number", "String", "75", "url", "1", "FactSamples", "labid");
            insertintoFieldsArray("Samples", "Spec ID", "Number", "String", "85", "chain", "1", "FactSamples", "specimenid");
            insertintoFieldsArray("Samples", "Receive Date ", "Date", "Date", "70", "url", "1", "FactSamples", "ReceiveDate");
            insertintoFieldsArray("Samples", "Collect date", "Date", "Date", "70", "url", "1", "FactSamples", "CollectDate");
            insertintoFieldsArray("Samples", "Donor ", "String", "String", "130", "url", "1", "FactSamples", "Donor");
            insertintoFieldsArray("Samples", "Collector", "String", "String", "130", "url", "1", "FactSamples", "Collector");
            insertintoFieldsArray("Samples", "Sample Result", "String", "List-SELECT Distinct SampleResult from FactSamples where SampleResult is not null ;", "50", "url", "1", "FactSamples", "SampleResult");
            //insertintoFieldsArray("Samples", "Portal URL", "String", "String", "275", "url", "0", "FactSamples", "'http://extranet/sites/' + customeridkey + extranetfilepath");
            //insertintoFieldsArray("Samples", "Patient OID ", "String", "String", "85", "url", "0", "FactSamples", "CONVERT(VARCHAR(50),DECRYPTBYKEY(OID))");
            insertintoFieldsArray("Samples", "Report  Date ", "Date", "Date", "80", "url", "0", "FactSamples", "ReportDate");
            insertintoFieldsArray("SampleDrugs", "Drug Name", "String", "List-select DrugName from FactSamples where drugsort is not null;", "200", "drug", "0", "FactSamples", "DrugName");
            insertintoFieldsArray("SampleDrugs", "Type", "String", "List-select distinct  DrugType from FactSamples where DrugType is not null ;", "85", "drug", "0", "FactSamples", "DrugType");
            insertintoFieldsArray("SampleDrugs", "Drug Result", "String", "List-select 'H' union select '.' union select 'C' union select 'P' union select '?' union select 'N' ; ", "50", "drug", "0", "FactSamples", "DrugResult");
            insertintoFieldsArray("SampleDrugs", "Drug Level", "String", "Number", "85", "drug", "0", "FactSamples", " ( case when DrugType='Header' then -1 else DrugResult end) ");

            //for conversion from selected fields into sql
            insertintoFilterArray("String", "equals", " = '$$$'");
            insertintoFilterArray("String", "not equals", " != '$$$'");
            insertintoFilterArray("String", "contains", " like '%$$$%'");
            insertintoFilterArray("String", "does not contain", " not like '%$$$%'");
            insertintoFilterArray("String", "starts with", " like '$$$%'");
            insertintoFilterArray("String", "ends with", " like '%$$$'");
            insertintoFilterArray("String", "is blank", " is null ");
            insertintoFilterArray("String", "is not blank", " is not null ");
            insertintoFilterArray("Number", "equals", " =$$$");
            insertintoFilterArray("Number", "greater than ", " >$$$");
            insertintoFilterArray("Number", "less than ", " <$$$");
            insertintoFilterArray("Number", "is blank", " is null ");
            insertintoFilterArray("Number", "is not blank", " is not null ");
            insertintoFilterArray("Date", "does not equal", " != '$$$'");
            insertintoFilterArray("Date", "equals ", " = '$$$'");
            insertintoFilterArray("Date", "after or on ", " >='$$$'");
            insertintoFilterArray("Date", "before or on ", " <='$$$'");
            insertintoFilterArray("List", "in", " in ('$$$')");
            insertintoFilterArray("List", "not in ", " not in ('$$$')");
            insertintoFilterArray("List", "is blank", " is null ");
            insertintoFilterArray("List", "is not blank", " is not null ");

          
            button2.Visible = false;
            StepSwitchbutton2.Visible = false; 

            //make step three and the grid view invisible, until we get the data from the database
            CartgroupBox.Visible = false;
            StepThreeGroupBox.Visible = false;
            ResultsdataGridView1.AllowUserToAddRows = false;          
   

            //fill the "choose fields" group of list boxes, the filter types the user will choose from
            FillChoiceListBoxes();
        }

        private void InitializeFilter()
        {
            int xStart = 10;
            int yStart = 30;

            int xSpot = xStart;
            int ySpot = yStart;

            int xMargin = 5;
            int yMargin = 5;

            //initialize the three columns of combo boxes in Step 2, and show them on the screen
            for (int i = 0; i < Field.Length; i++)
            {
                xSpot = xStart;

                //the actual filter field that is being selected like "Customer ID". setting it's properties
                Field[i] = new ComboBox();
                Field[i].Name = "Fields_" + i;
                Field[i].Location = new Point(xSpot, ySpot);

                //change the x coordinate for the next combo box. field's width + current
                //x coordinate + the margin between the combo boxes
                xSpot += xMargin + Field[i].Width;
                Field[i].TextChanged += Control_Clicks;
                Field[i].KeyPress += myCombo_KeyPress;

                //add the combo box to the UI group, making it visible on the screen
                StepTwoGroupBox.Controls.Add(Field[i]);

                //the conditional such as "contains" or "greater than".  setting it's properties
                Filter[i] = new ComboBox();
                Filter[i].Name = "Filter_" + i;
                Filter[i].Location = new Point(xSpot, ySpot);
                xSpot += xMargin + Filter[i].Width;
                Filter[i].Show();
                Filter[i].TextChanged += Control_Clicks;

                //add the combo box to the UI group, making it visible on the screen. 
                StepTwoGroupBox.Controls.Add(Filter[i]);

                //the value/input by the user.  could be a dropdown checkbox, if for example
                //the drug sample type is being selected
                Value[i] = new CheckBoxComboBox();
                Value[i].Name = "Value_" + i;
                Value[i].Location = new Point(xSpot, ySpot);
                Value[i].TextChanged += Control_Clicks;
                xSpot += xMargin + Value[i].Width;

                //add the check combo box to the UI group, making it visible on the screen.  setting it's properties
                StepTwoGroupBox.Controls.Add(Value[i]);
               
                //the picture box that displays nothing, a red X, or a checkmark showing
                //whether the filter is valid or not
                valid[i] = new PictureBox();
                valid[i].Name = "valid_" + i;
                valid[i].Location = new Point(xSpot, ySpot);
                valid[i].Size = new Size(20, 20);
                xSpot += xMargin + valid[i].Width;
                valid[i].Visible = true;

                //add the image to the UI
                StepTwoGroupBox.Controls.Add(valid[i]);

                //change the y coordinate for the three combo boxes of the next row
                ySpot += yMargin + Field[i].Height;
            }
        }

        private void myCombo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        //inserts filter type (like customer ID) properties into the array
        private void insertintoFieldsArray(String Category, String Field, String DataType, String FilterType, String Length, String Link,String selected , String dbTable, String dbField)
        {
            FieldsArray[FieldsArrayCount,0] = Category;
            FieldsArray[FieldsArrayCount,1] = Field;
            FieldsArray[FieldsArrayCount,2] = DataType;
            FieldsArray[FieldsArrayCount,3] = FilterType;
            FieldsArray[FieldsArrayCount,4] = Length;
            FieldsArray[FieldsArrayCount,5] = Link;
            FieldsArray[FieldsArrayCount, 6] = selected; // selected 
            FieldsArray[FieldsArrayCount, 7] = dbTable; 
            FieldsArray[FieldsArrayCount, 8] = dbField; 
            FieldsArrayCount++;           
        }
       
        //populates the array that contains the conditionals to be used in the second column of 
        //combo boxes in step 2 as well as the select query when the "Search" button gets clicked
        private void insertintoFilterArray(String FilterType, String FilterText, String SQL)
        {
            FilterArray[FilterArrayCount, 0] = FilterType;
            FilterArray[FilterArrayCount, 1] = FilterText;
            FilterArray[FilterArrayCount, 2] = SQL;
            FilterArrayCount++;
        }
       
        //fills the "choose fields" group of list boxes, the filter types the user will choose from
        private void FillChoiceListBoxes()
        {
            //clear the actual list boxes, before filling them
            CustomerListBox.Items.Clear();
            SamplesListBox.Items.Clear();
            SampleDrugsListBox.Items.Clear();
            SelectedItemsListBox.Items.Clear();

            for (int x = 0; x < Field.Length; x++)
            {
                //clear field values
                Field[x].Items.Clear();
            }


            for (int numRows = 0; numRows < FieldsArrayCount; numRows++)
            {
                if (FieldsArray[numRows, 6] == "1")
                {
                    //by default, put all the avaiable types of filters into the selected filters box
                    SelectedItemsListBox.Items.Add(FieldsArray[numRows, 1].ToString());

                    //FilterFieldcomboBox.Items.Add(FieldsArray[numRows, 1].ToString());
                    for (int x = 0; x < Field.Length; x++)
                    {
                        //place the filter types into the filter types column of comboboxes in step2
                        Field[x].Items.Add(FieldsArray[numRows, 1].ToString());
                    }
                }
                else
                {
                    ListBox CurrentLB = this.Controls.Find(FieldsArray[numRows, 0] + "ListBox", true).First() as ListBox;
                    CurrentLB.Items.Add(FieldsArray[numRows, 1].ToString());

                }
            }
            
        }



        //customer fields selection
        //"1" = add the filter type to the list of filters that show up in the drop down in step2
        //"0" = remove the filter type that's already in that list of filters
        private void CustomerListBox_DoubleClick_1(object sender, EventArgs e)
        {
            FieldClicked(CustomerListBox,"1");
        }
       
        //sample fields
        private void SamplesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FieldClicked(SamplesListBox, "1");
        }

        //sample drug fields
        private void SampleDrugsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FieldClicked(SampleDrugsListBox, "1");
        }
       
        //selected items box, double click removes the selected filter type
        private void SelectedItemsListBox_DoubleClick(object sender, EventArgs e)
        {
            FieldClicked(SelectedItemsListBox, "0");
        }
       
        private void FieldClicked(ListBox ChosenLB,String Value)
        {
            if (ChosenLB.SelectedItem != null)
            {
                FieldsArray[getFieldsArrayIndex(ChosenLB.SelectedItem.ToString()), 6] = Value;

                //refresh the list boxes after the change
                FillChoiceListBoxes();
            } 
        }
       

        //function to get the index of a particular filter type name as it appears in the list of filter types in step 2 and 
        //selected columns listbox
        private int getFieldsArrayIndex(String str)
        {
            for (int numRows = 0; numRows < FieldsArrayCount; numRows++)
            {
                if (FieldsArray[numRows, 1] == str)
                {                    
                    return numRows;
                }
            }
            return 0;
        }

        //event handlers of the filter dropdowns 
        private void Control_Clicks(object sender, EventArgs e)
        {
            Control control = (Control)sender;   
            String name = control.Name.ToString();
            String controlType = name.Split('_')[0];
            int index = int.Parse(name.Split('_')[1]);

            ComboBox cb = null;
            if (control.GetType().ToString() == "System.Windows.Forms.ComboBox")
            {
                 cb = control as ComboBox; 
            }


            //if one of the filter types is selected, update the filter conditional combo box for that row
            if (controlType == "Fields" && Field[index].Text != "" )
            {
                //filter type 

                //clear current filter dropdown
                Filter[index].Text = "";
                Filter[index].Items.Clear();
                int idx = getFieldsArrayIndex(Field[index].SelectedItem.ToString()); 
                String FilterType = FieldsArray[idx, 3].Split('-')[0];
                
                //populate the filter dropdown with new filters
                for (int i = 0; i < FilterArrayCount; i++)
                {
                    if (FilterType == FilterArray[i, 0])
                    {
                        Filter[index].Items.Add(FilterArray[i, 1]);
                    }
                }

                //filter value 
                if (FieldsArray[idx, 3] == "Date")
                {
                    Value[index].AllowDrop = false;

                    //if the filter type is a date, set the default user value as today's date
                    Value[index].Text = DateTime.Today.ToString("MM/dd/yyyy"); 
                    Value[index].DropDownHeight = 1;
                    Filter[index].Text = "after or on "; 
                }
                else if (FieldsArray[idx, 3].Substring(0, 4) == "List")
                {
                    fillListChoices(FieldsArray[idx, 3].Substring(5, FieldsArray[idx, 3].Length - 6), index);
                    Value[index].Text = "";
                    Value[index].DropDownHeight = 250;
                    Filter[index].Text = "in";
                }
                else
                {
                    Value[index].AllowDrop = false;
                    Value[index].Text = "";
                    Value[index].DropDownHeight = 1;
                    Filter[index].Text = "contains";
                }
            } 
            ValidateFilters(); 
        }

        //determine whether to show a red X or a green checkbox
        [TestMethod]
        private bool ValidateFilter(String fieldtxt, String filterTxt, String valuetxt)
        {
            //if the filter type is not blank
            if (fieldtxt != "")
            {
                if (FieldsArray[getFieldsArrayIndex(fieldtxt), 6] == "1")
                {
                    //if the conditional is not blank
                    if (filterTxt != "" )
                    {
                        int n;
                            //Assert.IsTrue(filterTxt.Contains("blank") || FieldsArray[getFieldsArrayIndex(fieldtxt), 3] != "Date" && valuetxt != "" || FieldsArray[getFieldsArrayIndex(fieldtxt), 3] == "Date" && isDate(valuetxt));

                            //if the type of the input by the user is a number, check to see if it is actually a number
                           if(FieldsArray[getFieldsArrayIndex(fieldtxt), 2] == "Number" && !int.TryParse(valuetxt, out n)  ){
                               return false;
                           } //if the type of the input is an text string, make sure it's not a number                               
                           else if (FieldsArray[getFieldsArrayIndex(fieldtxt), 2] == "String" && int.TryParse(valuetxt, out n))
                           {
                               return false;
                           }
                           else if ( (filterTxt.Contains("blank") || FieldsArray[getFieldsArrayIndex(fieldtxt), 3] != "Date" && valuetxt != "" 
                               || FieldsArray[getFieldsArrayIndex(fieldtxt), 3] == "Date" && isDate(valuetxt))  )
                             //if the input is a date, make sure it is a valid date
                           {    
                              return true; 
                           }
                        else
                        {
                            ThrowError("no valid value given");
                        }
                    }
                    else
                    {
                        ThrowError("no filter type was selected ");
                    }
                }
                else
                {
                    ThrowError("This Fields was not selected in the selected list ");
                }
            }
            else
            {
                ThrowError("no field was selected ");
            }
            return false; 
        }

        //check if date is legal
        private bool isDate(String dateTime)
        {
            //see if the date provided matches any of the formats, return true or false
            string[] formats = {  "M/d/yyyy", "MM/d/yyyy", "MM/dd/yyyy", "M/dd/yyyy", "M/d/yy", "MM/d/yy", "MM/dd/yy", "M/dd/yy" };
            DateTime parsedDateTime;         
            return DateTime.TryParseExact(dateTime, formats, new CultureInfo("en-US"),DateTimeStyles.None, out parsedDateTime);
        }

        private void ValidateFilters()
        {
            for (int x = 0; x < Field.Length; x++)
            {
                //passing the field type (such as Customer name),the conditional like "equal to",
                //and the value typed in by the user to the ValidateFilter() method.  if the input
                //is of the correct format, show the green checkmark, otherwise show a red X
                if (ValidateFilter(Field[x].Text, Filter[x].Text, Value[x].Text))
                {
                    valid[x].Image = System.Drawing.Image.FromFile("C:\\check.gif");
                    valid[x].Width = 51;
                }
                else
                {
                    if (Field[x].Text != "")
                    {
                        valid[x].Image = System.Drawing.Image.FromFile("C:\\x.png");
                        valid[x].Width = 50;
                   }
                    else
                    {
                       valid[x].Image = null;
                       valid[x].Width = 50; 
                    }
                }
            }
        }

        //create a csv out of selected list box fields.  for example, "customerID,labID,DrugType" etc
        private String toCSV(CheckedListBox LB)
        {
            StringBuilder sb = new StringBuilder();
            char[] characters = new char[] { ' ', ',' };
            String trimmedString;

            for (int i = 0; i < LB.CheckedItems.Count; i++)
            {
                sb.Append(LB.CheckedItems[i].ToString() + ",");
            }

            trimmedString = sb.ToString().TrimEnd(characters);
            return trimmedString;
        }

        //convert user's date into a date used in the SELECT query
        private int getSqlDate(DateTime date)
        {
            //formula for conversion.  for example, 2015/03/12 will be 20150000+300+12=20150312
            int returnval = date.Year * 10000 + date.Month * 100 + date.Day;
            return returnval;
        } 

        private void ThrowError(String errorMessage)
        {
            Console.Write(errorMessage+'\n');
        }
    
        //build the sql query        
        private String BuildQuery()
        {
            addCounter=0;
            //		OPEN SYMMETRIC KEY dimpatientskey DECRYPTION BY CERTIFICATE encryptcert
            String q = @"Select 100000X";
            int druglevel = 0; 

            //loop through the filter types selected by the user (using the fieldsArray), and add them to the SELECT statment
            for (int numRows = 0; numRows < FieldsArrayCount; numRows++)
            {
                if(FieldsArray[numRows,6]=="1")
                {
                    q+=", "+FieldsArray[numRows,8]+" ";
                    if ( FieldsArray[numRows,0] == "SampleDrugs")
                    {
                        druglevel = 1; 
                    }

                }
            }
            q = q.Replace("100000X,", " ");

            //using a hardcoded URL value for this demo version.  the release version would have the actual PDF's URL
            q += ", \"http://www.asfsct.org/compass/sample/pdf/readingsdf.pdf \" as 'url' ";
      
            //release version's SELECT query

            //", CONCAT('http://externalsite.com/', CustomerID , '.pdf') as 'url' , CustomerID as 'coc', 1 as 'primaryMarketHide' ";

            /* q += @" ,'http://extranet/sites/' + customeridkey + extranetfilepath as url
                    , c.primaryMarket as  primaryMarkethide 
                    , (select  top 1  'http://chains/Chains/'+leafname   from wss_content_chains.dbo.AllUserData u   inner join wss_content_chains.dbo.alldocs d on u.tp_DocId = d.id and u.tp_DeleteTransactionId = '' where u.tp_listid = 'B42B3A7B-54D1-4348-BCAD-679B9098C560'  and  nvarchar7 = concat(s.labidkey,'')) as coc 
                    ,concat(concat((select count(sequenceid) from factschedules fs2 where extranetfilepath is not null and   fs2.labid=fs.labid and   fs2.sequenceid<=fs.sequenceid ) 
				  ,'/'), (select count(sequenceid) from factschedules fs2 where extranetfilepath is not null and   fs2.labid=fs.labid ) ) as ReportVersion
                         FROM  FactSamples s
                    left outer  join (select * from factschedules where extranetfilepath is not null )  fs on s.labidkey=fs.labid
                    left outer  join dimcustomers c on c.customerid=s.customeridkey 
                    left outer  join dimdate rd on rd.datesk=s.ReceiveDateKey
                    left outer  join dimdate cd on cd.datesk=s.CollectDateKey
                    left outer  join DimPatients p on p.PatientID=s.PatientSequenceID 
                 ";
                if (druglevel == 1)
               {
                   q += @"left outer join FactSampleDetails sd on sd.LabIDKey=s.LabIDKey
                            left outer join dimdrugs d on d.DrugID=sd.DrugIDKey
                        ";
               }
            */
           q += " from FactSamples";
             
           //add conditionals to the WHERE clause, using the values entered into the user's filter values combo boxes in step2
           int First = 1;
           String FilterTemp = "";
           String ValueTemp = "";
           for (int i = 0; i < filterCount; i++)
           {
               FilterTemp=Filter[i].Text;
               ValueTemp=Value[i].Text;
               if (valid[i].Width == 51) 
               {
                   if (First == 1) { q += " WHERE 1=1 "; First = 0;  }
                   if (isDate(Value[i].Text))
                   {
                       q += "and " + CSVtoSQL(Field[i].Text + "_" + Filter[i].Text + "_" + getSqlDate(DateTime.Parse(Value[i].Text))); 
                   }
                   else
                   { 
                        String tempq = "and " + CSVtoSQL(Field[i].Text + "_" + Filter[i].Text + "_" + Value[i].Text.Replace(", ", ","));
                        
                       if (Filter[i].Text.Contains("equals") && Value[i].Text.Contains(","))
                        {
                            tempq=tempq.Replace(",", "','");
                            tempq = tempq.Replace("=", " in (");
                            tempq = tempq.Replace("!", " not ");
                            tempq += ")"; 
                        }

                       q += tempq; 
                   }
                   
               }
               
           }

           //the Latest Reports Checkbox is included in the release version
           /*
           if (latestreportscheckBox1.Checked)
           {
               q += @" and 
				(select count(sequenceid) from factschedules fs2 where extranetfilepath is not null and   fs2.labid=fs.labid and   fs2.sequenceid<=fs.sequenceid ) 
				  = (select count(sequenceid) from factschedules fs2 where extranetfilepath is not null and   fs2.labid=fs.labid )";
           }*/


           q = q.Replace("WHERE 1=1 and ", " WHERE ");
   
           return q; 
        }
        //translate the csv string to what should be in sql, like quotes around fields
        private  String CSVtoSQL(String csv) 
        {
            String Field = csv.Split('_')[0].Replace("'", "''");
            String FieldSQL = FieldsArray[getFieldsArrayIndex(Field), 8];
            String Filtertext = csv.Split('_')[1].Replace("'", "''");
            String Filtervalue = csv.Split('_')[2].Replace("'", "''"); 

            String Filtertype =  FieldsArray[getFieldsArrayIndex(Field),3]  ;

            for (int numRows = 0; numRows < FilterArrayCount; numRows++)
            {
                if (FilterArray[numRows, 0] == Filtertype.Split('-')[0] && FilterArray[numRows, 1] == Filtertext) 
                {
                    if (Filtertype.Split('-')[0] == "List")
                    {
                        return (FieldSQL + " " + FilterArray[numRows, 2].Replace("$$$", Filtervalue)).Replace(",", "','");
                    }
                    else
                    {
                        return FieldSQL + " " + FilterArray[numRows, 2].Replace("$$$", Filtervalue);
                    }
                }
            }

            return "NOT FOUND"+csv ;

        }

        private void fillListChoices(String query, int FilterIndex)
        {
 

            //connection string for the MySQL dirver for opening the connection, which includes the credentaisl and the database name
            string strConnection = "host=db4free.net;user=dbadmin;password=dbadmin;database=datawarehouse490;";


            MySql.Data.MySqlClient.MySqlConnection conn;
            MySql.Data.MySqlClient.MySqlCommand cmd;

            conn = new MySql.Data.MySqlClient.MySqlConnection();
            cmd = new MySql.Data.MySqlClient.MySqlCommand();

            conn.ConnectionString = strConnection;

            try
            {
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = query;  //"INSERT INTO FactSamples VALUES("++");";

                //prepare the statement
                cmd.Prepare();         

               MySqlDataReader reader =  cmd.ExecuteReader();

             
                Value[FilterIndex].Items.Clear();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Value[FilterIndex].Items.Add(reader.GetString(0));
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }



            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error " + ex.Number + " has occurred: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        //the function is used to connect to the database and executing the select query
        private void DatabaseCall(String Query)
        {

            string strConnection = "host=db4free.net;user=dbadmin;password=dbadmin;database=datawarehouse490;";

            MySql.Data.MySqlClient.MySqlConnection conn;
            MySql.Data.MySqlClient.MySqlCommand cmd;

            conn = new MySql.Data.MySqlClient.MySqlConnection();
            cmd = new MySql.Data.MySqlClient.MySqlCommand();

            conn.ConnectionString = strConnection;

            try
            {
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = Query;  //"INSERT INTO FactSamples VALUES("++");";
                cmd.Prepare();

                //exeucte the select query, get the data
                MySqlDataReader reader =  cmd.ExecuteReader();

                //load the results of the sql query into a DataTable object, later to be used as the GridView's DataSource
                DataTable table = new DataTable();
                table.Load(reader);
                 

                ////////// START OF INSERT
                
                //populate the data grid

                //add the "ADD" and "View" buttons to the first four columns of the grid
                DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                col.UseColumnTextForButtonValue = true;
                col.Text = "ADD";
                col.Name = "Report";
                col.Width = 45;
                col.Frozen = true;
                ResultsdataGridView1.Columns.Add(col);

                DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
                col2.UseColumnTextForButtonValue = true;
                col2.Text = "View";
                col2.Name = "Report";
                col2.Width = 45;
                col2.Frozen = true;
                ResultsdataGridView1.Columns.Add(col2);


                DataGridViewButtonColumn col4 = new DataGridViewButtonColumn();
                col4.UseColumnTextForButtonValue = true;
                col4.Text = "ADD";
                col4.Name = "Chain";
                col4.Width = 45;
                col4.Frozen = true;
                ResultsdataGridView1.Columns.Add(col4);

                DataGridViewButtonColumn col3 = new DataGridViewButtonColumn();
                col3.UseColumnTextForButtonValue = true;
                col3.Text = "View";
                col3.Name = "Chain";
                col3.Width = 45;
                col3.Frozen = true;
                ResultsdataGridView1.Columns.Add(col3);

                ResultsdataGridView1.DataSource = table;
                
                int i = 4;

                //loop through the filter types array, and name the grid's columns with them
                for (int numRows = 0; numRows < FieldsArrayCount; numRows++)
                {
                   //if the filter type is used ("1" means it's used by the user)
                    if (FieldsArray[numRows, 6] == "1")
                    {

                        if (FieldsArray[numRows, 2] == "Date")
                        {
                            //ReplaceWithDateColumn(i, FieldsArray[numRows, 1], table);

                        }

                        ResultsdataGridView1.Columns[i].HeaderCell.Value = FieldsArray[numRows, 1];
                        ResultsdataGridView1.Columns[i].Name = FieldsArray[numRows, 1];
                        ResultsdataGridView1.Columns[i].Width = int.Parse(FieldsArray[numRows, 4]);
                        ResultsdataGridView1.Columns[i].ReadOnly = true;

                        i++;
                    }

                }
                
                //don't show the URL column in the demo version, visible in the release version
                ResultsdataGridView1.Columns["url"].Visible = false;

                //comment out the lines that populate the primaryMarkethide and coc columns,
                //as those are only used in the release version
                //ResultsdataGridView1.Columns["primaryMarkethide"].Visible = false;
                //ResultsdataGridView1.Columns["coc"].Visible = false;


                ////////// END OF INSERT

                //set the grid's cells with what was recieved using the select query
                ResultsdataGridView1.DataSource = table;
           
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error " + ex.Number + " has occurred: " + ex.Message,  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
 


            //make the step3 group of UI controls visible
            StepThreeGroupBox.Visible = true;
            CartgroupBox.Visible = true;


            //The code below is used in the release version and not in the test/demo version

            /*SqlConnection connection = new SqlConnection("Server=SQL2012;Database=Datawarehouse;User Id=bdeyoungApplicationsReadOnly;Password=5QIpk9ijpIHHHJqjxL45;Connect Timeout=500");
           

            using (connection)
            {
                SqlCommand command = new SqlCommand(Query, connection);
                connection.Open();
                
                SqlDataReader reader = command.ExecuteReader();

                DataTable table = new DataTable();
                table.Load(reader);

                reader.Close();
                reader.Dispose();
                connection.Close();
                connection.Dispose();
                command.Dispose();
                command = null;
                ResultsdataGridView1.DataSource = null;
                this.ResultsdataGridView1.Rows.Clear();
                ResultsdataGridView1.Columns.Clear();
                ResultsdataGridView1.Refresh();

                DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                col.UseColumnTextForButtonValue = true;
                col.Text = "ADD";
                col.Name = "Report";
                col.Width = 45;
                col.Frozen = true; 
                ResultsdataGridView1.Columns.Add(col);

                DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
                col2.UseColumnTextForButtonValue = true;
                col2.Text = "View";
                col2.Name = "Report";
                col2.Width = 45;
                col2.Frozen = true; 
                ResultsdataGridView1.Columns.Add(col2);


                DataGridViewButtonColumn col4 = new DataGridViewButtonColumn();
                col4.UseColumnTextForButtonValue = true;
                col4.Text = "ADD";
                col4.Name = "Chain";
                col4.Width = 45;
                col4.Frozen = true;
                ResultsdataGridView1.Columns.Add(col4);

                DataGridViewButtonColumn col3 = new DataGridViewButtonColumn();
                col3.UseColumnTextForButtonValue = true;
                col3.Text = "View";
                col3.Name = "Chain";
                col3.Width = 45;
                col3.Frozen = true; 
                ResultsdataGridView1.Columns.Add(col3);

                ResultsdataGridView1.DataSource = table;

                int i = 4;
                for (int numRows = 0; numRows < FieldsArrayCount; numRows++)
                {
                    if (FieldsArray[numRows, 6] == "1")
                    {

                        if (FieldsArray[numRows, 2] == "Date")
                        {
                            ReplaceWithDateColumn(i, FieldsArray[numRows, 1], table);

                        }
                        
                        ResultsdataGridView1.Columns[i].HeaderCell.Value = FieldsArray[numRows, 1];
                        ResultsdataGridView1.Columns[i].Name = FieldsArray[numRows, 1];
                        ResultsdataGridView1.Columns[i].Width = int.Parse(FieldsArray[numRows, 4]);
                        ResultsdataGridView1.Columns[i].ReadOnly = true;

                        i++;
                    }

                }

                 
                ResultsdataGridView1.Columns["url"].Visible = false;
                ResultsdataGridView1.Columns["primaryMarkethide"].Visible = false;
                ResultsdataGridView1.Columns["coc"].Visible = false;

                connection.Close();
            }
            
            
            

            StepThreeGroupBox.Visible = true;
            CartgroupBox.Visible = true;



            CheckBlankButtons();
            */
        }



        private void ReplaceWithDateColumn(int index, String ColumnName, DataTable table)
        {
            table.Columns.Add(new DataColumn(ColumnName, typeof(DateTime)));
            DataColumn newCol = table.Columns[ColumnName];
            for (int row = 0; row < ResultsdataGridView1.RowCount; row++)
            {
                if (ResultsdataGridView1.Rows[row].Cells[index].Value.ToString() != "" && ResultsdataGridView1.Rows[row].Cells[index].Value.ToString().Length == 8)
                {
                    String Date1 = ResultsdataGridView1.Rows[row].Cells[index].Value.ToString();
                    int year = int.Parse(Date1.Substring(0, 4));
                    int month = int.Parse(Date1.Substring(4, 2));
                    int day = int.Parse(Date1.Substring(6, 2));
                    ResultsdataGridView1.Rows[row].Cells[ColumnName].Value = new DateTime(year, month, day);
                }
            }

            
            ResultsdataGridView1.Columns[index].Visible = false;
            ResultsdataGridView1.Columns[ColumnName].DisplayIndex = index; 

        }

        //this function loops through the rows of recieved data, and if the URL for the document is not available,
        //set the button text to blank so that users won't be able to add the PDF to their selected PDF list.  
        //only available in the release version
        private void CheckBlankButtons()
        {
            for (int numRows = 0; numRows < ResultsdataGridView1.Rows.Count; numRows++)
            {
                if (ResultsdataGridView1.Rows[numRows].Cells["url"].Value.ToString() == "")
                {
                    DataGridViewButtonCell b = ResultsdataGridView1.Rows[numRows].Cells[0] as DataGridViewButtonCell;                   
                    b.UseColumnTextForButtonValue = false;
                    b = ResultsdataGridView1.Rows[numRows].Cells[1] as DataGridViewButtonCell;
                    b.UseColumnTextForButtonValue = false;
                }
 
                if (ResultsdataGridView1.Rows[numRows].Cells["coc"].Value.ToString() == "")
                 {
                    DataGridViewButtonCell b = ResultsdataGridView1.Rows[numRows].Cells[2] as DataGridViewButtonCell;
                    b.UseColumnTextForButtonValue = false;
                    b = ResultsdataGridView1.Rows[numRows].Cells[3] as DataGridViewButtonCell;
                    b.UseColumnTextForButtonValue = false;
                 }
            }
        }


        //reset various listbox fields
        private void StepOneResetButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < SelectedItemsListBox.Items.Count; i++)
            {
                FieldsArray[getFieldsArrayIndex(SelectedItemsListBox.Items[i].ToString()), 6] = "0";
            }
            FillChoiceListBoxes();

        }

        private void StepTwoResetButton_Click(object sender, EventArgs e)
        {
        }


        //the Search button click event handler
        [TestMethod]
        private void StepTwoNextButton_Click(object sender, EventArgs e)
        {
            int x;
            for (x = 0; x < Field.Length; x++){

                //checking filter values being valid as part of the test
                int m;
                bool containsID = FieldsArray[getFieldsArrayIndex(Field[x].Text), 1].Contains("ID");
                bool isNumber = int.TryParse(Value[x].Text, out m);
           
                //checks the validity of user's input as a Unit Test
                Assert.IsTrue(Filter[x].Text=="" || (containsID  && isNumber ) ||  (FieldsArray[getFieldsArrayIndex(Field[x].Text), 3] == "Date" && isDate(Value[x].Text)) ||
                    (!containsID && FieldsArray[getFieldsArrayIndex(Field[x].Text), 3] == "String" && !isNumber));
                    
            }


            //get the data from the database
            ValidateFilters(); 
            Console.WriteLine(BuildQuery());
            DatabaseCall(BuildQuery());

            //set the number of rows returned to the text label below the grid view
            countfoundlabel1.Text = ResultsdataGridView1.RowCount.ToString() + " Results Found"; 

        }

        //export to excel button
        private void Exportbutton_Click(object sender, EventArgs e)
        {
            String direc = Directory.GetCurrentDirectory();
            System.Console.WriteLine("directory is " + direc);
            String Timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);

            //String dir = "\\\\sbs1\\Shared\\Temp\\" + Timestamp + "\\"; release version directory
            String dir = "C:\\excell\\";
            String FinalPath = dir + "QueryResults.xls";
            System.IO.Directory.CreateDirectory(dir); 
            export_datagridview_to_excel(ResultsdataGridView1, FinalPath); // @"//sbs1/Shared/Temp/test.xls");

            //open excel once data is written to it
            System.Diagnostics.Process.Start(FinalPath);
        }

        private void Download_Click(object sender, EventArgs e)
        {
            


            String filename = "Default.pdf";
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Save pdf Files";
            //saveFileDialog1.CheckFileExists = true;
            //saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.Filter = "Pdf Files|*.pdf";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = saveFileDialog1.FileName;
            }

            String FinalName = DownloadAndMerge();

            System.IO.File.Copy(FinalName, filename);
            System.IO.File.Delete(FinalName);
        }

        private void Print_Click(object sender, EventArgs e)
        {
            String FinalName = DownloadAndMerge();
            System.Diagnostics.Process.Start(FinalName);
        }

        private void Email_Click(object sender, EventArgs e)
        {
            //merge the PDF's and open outlook with the merged PDF file added as an attachment.  the demo version
            //only merges the PDFs in the C:\fflpdf directory. it does not download any PDFs. the release version does
            //DownloadAndMerge();
            String FinalName = DownloadAndMerge();    //"\\\\sbs1\\Shared\\Temp\\2014-07-03_09-22-45-AM\\LabReports.pdf";  //
            System.Diagnostics.Process.Start("C:\\Program Files\\Microsoft Office 15\\root\\office15\\OUTLOOK.EXE", "/a C:\\fflpdf\\SearchResults.pdf /m name@address.com");                
        }

        [TestMethod]
        private String DownloadAndMerge()
        {
            //the demo version does not download the pdf's using the vbscript provided, but merges the pdf's in the
            // fflpdf directory into one large PDF using iTextSHarp

            //if there are items in list box 
            // create folder timestamo in //sbs1/Temp 
            //for each item in list box 
            //download and rename 1,2,3,4
            //merge the whole folder 
            String Timestamp = "2014-07-03_09-22-45-AM";
            String dir = "C:\\fflpdf\\";///*"\\\\sbs1\\Shared\\Temp\\"*/ +Timestamp + "\\";
            String FinalPath = dir + "SearchResults.pdf";

            //listbox1 is the list of files/URLS
            if (listBox1.Items.Count > 0)
            {
                Timestamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
                dir = "C:\\fflpdf\\";// +Timestamp + "\\"; ; //"\\\\sbs1\\Shared\\Temp\\"
                FinalPath = dir + "SearchResults.pdf";

                //Assert.AreEqual(dir, "C:\\fflpdf\\2014-07-03_09-22-45-AM\\");
                System.IO.Directory.CreateDirectory(dir);
            }
             ENCRYPT_EMAIL = false; 

            //traverse through all the files in the search results
            //download the pdf from the site in the link, put them in the same direcotry, and the ntell the script 
            //the actual path
            //used in the release version, not in this demo version
            /*for (int i = 0; i < listBox1.Items.Count; i++)
            {
                Process scriptProc = new Process();
                //scriptProc.StartInfo.FileName = @"\\10097-pc\c$\Users\bdeyoung\Desktop\saver2.vbs";
                scriptProc.StartInfo.FileName = @"\\sbs1\shared\Temp\saver2.vbs";
                scriptProc.StartInfo.Arguments = "\"" + listBox1.Items[i].ToString().Split(',')[0] + "\" \"" + i.ToString("0000000000") + "\" \"" + dir + "\"";
                scriptProc.StartInfo.UseShellExecute = true;
                scriptProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; 
                scriptProc.Start();
                scriptProc.WaitForExit();
                scriptProc.Close();
               // MessageBox.Show(listBox1.Items[i].ToString().Split(',')[1]); 
                if (listBox1.Items[i].ToString().Split(',')[1] == "Therapeutic") { ENCRYPT_EMAIL = true; }
            }*/

            Merge(dir, FinalPath);
            return FinalPath;
        }

        //merge the pdf's with the folder pdf provided in the parameter
        [TestMethod]      
        private void Merge(String Folder,String EndPath )
        {
            File.Delete("c:\\fflpdf\\SearchResults.pdf");
            string[] lstFiles = Directory.GetFiles(Folder, "*.pdf");
            for (int y = 0; y < lstFiles.Count(); y++)
            {
                Console.WriteLine(lstFiles[y]);
            }
           
            PdfReader reader = null;
            Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage;
            string outputPdfPath = EndPath;


            sourceDocument = new Document();
            pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

            //Open the output file
            sourceDocument.Open();
            //Assert.AreEqual(Folder, EndPath);
       
            try
            {
                //Loop through the files list in the directory
                for (int f = 0; f < lstFiles.Length; f++)
                {
                    int pages = get_pageCcount(lstFiles[f]);
                    //Assert.IsTrue(pages > 8);
                    reader = new PdfReader(lstFiles[f]);

                    //Add pages of current file
                    for (int i = 1; i <= pages; i++)
                    {
                        importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                        pdfCopyProvider.AddPage(importedPage);
                    }

                    reader.Close();
                   //File.Delete(lstFiles[f]); deletes the pdf that it just added to the big PDF
                }

                //At the end save the output file
                sourceDocument.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //gets the number of pages from the PDF file
        private int get_pageCcount(string file)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(file)))
            {
                Regex regex = new Regex(@"/Type\s*/Page[^s]");
                MatchCollection matches = regex.Matches(sr.ReadToEnd());

                return matches.Count;
            }
        }


        //opens the PDF document in the browser.  the release version opens the actual medical documents
        private void openUrl(string url)
        {
            if (url != "")
            {
                ProcessStartInfo sInfo = new ProcessStartInfo(url);
                Process.Start(sInfo);
            }

        }
         
        //removes a PDF no longer wanted by the user
        private void Removebutton4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) { return; }
            listBox1.Items.Remove(listBox1.SelectedItems[0]);
        }

        //removes all PDFs if the user wants to start over adding documents
        private void removeallbutton5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        // Move Up button event handler
        private void LisboxMoveUp_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            { return; }


            var idx = listBox1.SelectedIndex;
            var elem = listBox1.SelectedItem;
            if (idx > 0)
            {
                listBox1.Items.RemoveAt(idx);
                listBox1.Items.Insert(idx - 1, elem);
                listBox1.SetSelected(idx - 1, true);
            }
        }

        // Move Down button event handler
        private void ListboxMoveDown_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            { return; }


            var idx = listBox1.SelectedIndex;
            var elem = listBox1.SelectedItem;
            if (idx < listBox1.Items.Count - 1)
            {
                listBox1.Items.RemoveAt(idx);
                listBox1.Items.Insert(idx + 1, elem);
                listBox1.SetSelected(idx + 1, true);
            }
        }

        //the "View" button underneath the PDF list box.  opens the browser with the selected document
        private void ViewURL_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            { return; }

            openUrl(listBox1.SelectedItem.ToString().Split(',')[0] );
        }

        //opens the PDF in the PDF listbox on double click
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            { return; }

            openUrl(listBox1.SelectedItem.ToString());
        }

        //event handler for the "ADD" and "View" buttons in the grid view.  adds a document to the list of PDFs below the
        //data grid, or opens it in the browser
        private void ResultsdataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          
            if (e.RowIndex == -1) { return; }
            if (e.ColumnIndex == -1) { return; }
            Int32 selectedRowCount = ResultsdataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);
            String url = "http://www.act.org/compass/sample/pdf/reading.pdf"; //String url = ResultsdataGridView1.Rows[e.RowIndex].Cells["url"].Value.ToString();           
            String Chain = "http://www.pdf995.com/samples/pdf.pdf";
  
            //market property is not used in the demo/test version
            String market = ""; //ResultsdataGridView1.Rows[e.RowIndex].Cells["primaryMarkethide"].Value.ToString();


            //index 0="ADD",1="View",2="ADD",3="View", the rest are not buttons but data cells
            if (e.ColumnIndex == 0)
                {
                    if (url != "")
                    {
                        listBox1.Items.Add(url + "," + market);
                    }

                }
                else if (e.ColumnIndex == 1)
                {
                    if (url != "")
                    {
                       openUrl(url);
                    }
                }
                else if (e.ColumnIndex == 2) 
                {
                    if (Chain != "")
                    {
                        listBox1.Items.Add(Chain + "," + market);
                    }
                }
                else if (e.ColumnIndex == 3)
                {
                    if (Chain != "")
                    {
                        openUrl(Chain);
                    }
                }
                else { } 
            

        }

        private void filterdateTimePicker1_ValueChanged(object sender, EventArgs e)
        {         
        }

        private void filterdataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            CheckBoxComboBox cb = new CheckBoxComboBox();
            Location = new Point(500,500);
            cb.Visible = true;
            cb.Show();
            cb.BringToFront();
            cb.Focus();
            Controls.Add(cb); 
            
            
        }
       
        //write the data in the grid view to an excel spreadsheet
        public void export_datagridview_to_excel(DataGridView dgv, string excel_file)
        {
            int cols;
            //open file 
            //StreamWriter wr = new StreamWriter(excel_file);
            using (StreamWriter wr = File.AppendText(excel_file))
            {
                //determine the number of columns and write columns to file 
                cols = dgv.Columns.Count;
                //set i to 4 because the first four columns are buttons
                for (int i = 4; i < cols; i++)
                {
                    if (dgv.Columns[i].Visible == true)
                    {
                        wr.Write(dgv.Columns[i].Name.ToString().ToUpper() + "\t");
                    }
                }

                wr.WriteLine();

                //write rows to excel file 
                for (int i = 0; i < (dgv.Rows.Count); i++)
                {
                    //set j to 4 because the first four columns are buttons
                    for (int j = 4; j < cols; j++)
                    {
                        if ( dgv.Columns[j].Visible == true) 
                        {
                            if (dgv.Rows[i].Cells[j].Value != null)
                                wr.Write(dgv.Rows[i].Cells[j].Value + "\t");
                            else
                            {
                                //blank field
                                wr.Write("\t");
                            }   
                        }

                    }
                    //after writing the whole row to the spreadsheet, start over with a new line 
                    //and write a new row
                    wr.WriteLine();
                }

                //close file 
                wr.Close();
            }
        } 
        
        private void DrawFilterLines()
        {
          
            for (int i = 0; i < Field.Length; i++)
            {   
                    Field[i].Show();
                    Filter[i].Show();
                    Value[i].Show();
                    valid[i].Show();                    
            }
        }

        private void ResultsdataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CheckBlankButtons();
        }

        //when the Clear Values button gets clicked
        private void ResetFilters_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Value.Length; i++)
            {
                //set the text to blank and if the value is a check and combo box, clear the checkbox selection
                Value[i].Text = "";
                Value[i].ClearSelection();
            }
        }

        //when the Clear Filters button gets clicked
        private void resetFilters2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Value.Length; i++)
            {
                //set the text to blank and if the value is a check and combo box, clear the checkbox selection
                Value[i].Text = "";
                Value[i].ClearSelection();

                //set the field type combo box text to blank, and reset the selected item index
                Field[i].Text = "";
                Field[i].SelectedIndex = -1;

                //set the conditional filter combo box text to blank, and reset the selected item index
                Filter[i].Text = "";
                Filter[i].SelectedIndex = -1; 
            }
        }

        //event handlers below are commented out because they're part of the official release,
        //and not part of the senior design demo
        /*
        private void button1_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage(WindowsIdentity.GetCurrent().Name.Replace("FORENSICFLUIDS\\", "") + "@forensicfluids.com", "bdeyoung@forensicfluids.com");
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            client.Host = "mail.forensicfluids.com";
            //mail.CC.Add(new MailAddress(textBox4.Text));
            mail.Subject = "LAB REPORT SERACH TOOL  query  ";
            mail.Body = BuildQuery(); 

            

            client.Send(mail);
            MessageBox.Show("Message Sent to IT");
 
 
        }

        private void addallreportsbutton2_Click(object sender, EventArgs e)
        {
            String url = "";
            String market = "";

            for (int numRows = 0; numRows < ResultsdataGridView1.Rows.Count; numRows++)
            {
                if (ResultsdataGridView1.Rows[numRows].Cells["url"].Value.ToString() != "")
                {
                    url = ResultsdataGridView1.Rows[numRows].Cells["url"].Value.ToString();
                   // market = ResultsdataGridView1.Rows[numRows].Cells["primaryMarkethide"].Value.ToString();
                    listBox1.Items.Add(url + "," + market);
                }
            }
        }

        private void addallchainsbutton3_Click(object sender, EventArgs e)
        {
            String Chain = "";
            String market = "";

            for (int numRows = 0; numRows < ResultsdataGridView1.Rows.Count; numRows++)
            {
                if (ResultsdataGridView1.Rows[numRows].Cells["coc"].Value.ToString() != "")
                {
                    Chain = ResultsdataGridView1.Rows[numRows].Cells["coc"].Value.ToString();
                    market = ResultsdataGridView1.Rows[numRows].Cells["primaryMarkethide"].Value.ToString();
                    listBox1.Items.Add(Chain + "," + market);
                }
            }
        }

        private void addallbothbutton4_Click(object sender, EventArgs e)
        {
            String url = "";
            String Chain = "";
            String market = "";

            for (int numRows = 0; numRows < ResultsdataGridView1.Rows.Count; numRows++)
            {
                if (ResultsdataGridView1.Rows[numRows].Cells["url"].Value.ToString() != "")
                {
                    url = ResultsdataGridView1.Rows[numRows].Cells["url"].Value.ToString();
                    market = ResultsdataGridView1.Rows[numRows].Cells["primaryMarkethide"].Value.ToString();
                    listBox1.Items.Add(url + "," + market);
                }

                if (ResultsdataGridView1.Rows[numRows].Cells["coc"].Value.ToString() != "")
                {
                    Chain = ResultsdataGridView1.Rows[numRows].Cells["coc"].Value.ToString();
                    market = ResultsdataGridView1.Rows[numRows].Cells["primaryMarkethide"].Value.ToString();
                    listBox1.Items.Add(Chain + "," + market);
                }
            }

        }
        */


        private void button2_Click(object sender, EventArgs e)
        {
            StepOneGroupBox.Visible = true;
            StepOneGroupBox.BringToFront();
            StepTwoGroupBox.SendToBack();
            StepTwoGroupBox.Visible = false;
            button2.Enabled = false;
            StepSwitchbutton2.Enabled = true;
        }

        private void StepSwitchbutton2_Click(object sender, EventArgs e)
        {
            StepTwoGroupBox.Visible = true; 
            StepOneGroupBox.Visible = false;
            StepTwoGroupBox.BringToFront();
            StepOneGroupBox.SendToBack();
            StepSwitchbutton2.Enabled = false;
            button2.Enabled = true; 

        }

        private void QueryBuilder_SizeChanged(object sender, EventArgs e)
        {
            bool isBigWindow = true;
            int Spacer = 10; 
            if ( this.Width < 1000)
            {
                isBigWindow = false;
                StepTwoGroupBox.Location = new Point(StepOneGroupBox.Location.X, StepOneGroupBox.Location.Y);

                button2.Enabled = false;
                StepSwitchbutton2.Enabled = true;
                

            }
            else
            {
                isBigWindow = true;
                StepTwoGroupBox.Location = new Point(StepOneGroupBox.Location.X+StepOneGroupBox.Width + Spacer, StepOneGroupBox.Location.Y + Spacer); 
                
            }
            StepOneGroupBox.Visible = true; 
            StepTwoGroupBox.Visible = isBigWindow; 
            button2.Visible = !isBigWindow;
            StepSwitchbutton2.Visible = !isBigWindow;

            
            StepThreeGroupBox.Width =  this.Width - 5;
            ResultsdataGridView1.Width = StepThreeGroupBox.Width - 15; 


        }

        private void QueryBuilder_Load(object sender, EventArgs e)
        {

        }

        private void Customerlabel_Click(object sender, EventArgs e)
        {

        }
    }
}
