using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FTN.Common;


namespace Klijent
{
    public partial class Form1 : Form
    {
        public GDA gda = new GDA();
        ModelResourcesDesc modelRD = new ModelResourcesDesc();

        public List<ModelCode> listaPropertija = new List<ModelCode>();
        public List<long> lista_getVal = new List<long>();
        public List<long> lista_getExVal = new List<long>();
        public List<long> lista_getRelVal = new List<long>();
        public List<string> listaString = new List<string>();
        public List<ModelCode> properties = new List<ModelCode>();
        public List<string> distinct = new List<string>();
        public static List<long> pomocna = new List<long>();
        public string tekst;
        public StringBuilder sb;
        public Form1()
        {
            InitializeComponent();

            comboBox3.Items.Add("CONNECTIVITYNODE");
            comboBox3.Items.Add("TERMINAL");
            comboBox3.Items.Add("POWERTR");
            comboBox3.Items.Add("POWTREND");
            comboBox3.Items.Add("RATIOTAPCHANGER");

            comboBox2.Items.Add("CONNECTIVITYNODE");
            comboBox2.Items.Add("TERMINAL");
            comboBox2.Items.Add("POWERTR");
            comboBox2.Items.Add("POWTREND");
            comboBox2.Items.Add("RATIOTAPCHANGER");

            comboBox4.Items.Add("CONNECTIVITYNODE");
            comboBox4.Items.Add("TERMINAL");
            comboBox4.Items.Add("POWERTR");
            comboBox4.Items.Add("POWTREND");
            comboBox4.Items.Add("RATIOTAPCHANGER");
        }
        

        #region tab1
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            lista_getVal.Clear();
            richTextBox1.Text = string.Empty;
            comboBox1.Items.Clear();

            switch (comboBox3.SelectedItem.ToString())
            {
                case "CONNECTIVITYNODE":
                    lista_getVal = gda.GetExtentValues(ModelCode.CONNECTIVITYNODE);
                    break;
                case "TERMINAL":
                    lista_getVal = gda.GetExtentValues(ModelCode.TERMINAL);
                    break;
                case "POWERTR":
                    lista_getVal = gda.GetExtentValues(ModelCode.POWERTR);
                    break;
                case "POWTREND":
                    lista_getVal = gda.GetExtentValues(ModelCode.POWTREND);
                    break;
                case "RATIOTAPCHANGER":
                    lista_getVal = gda.GetExtentValues(ModelCode.RATIOTAPCHANGER);
                    break;
            }

            foreach (var v in lista_getVal) 
            {
                comboBox1.Items.Add(v);
            }
            comboBox1.SelectedIndex = 0;
            comboBox1.Enabled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
            if (comboBox1.SelectedItem != null)
            {
                try
                {
                    long gid = (long)comboBox1.SelectedItem;
                    ResourceDescription rd = gda.GetValues(gid);
                    richTextBox1.Text = gda.IspisUTextBox(rd);
                }
                catch
                {
                    richTextBox1.Text = "ERROR";
                }
            }

        }

        #endregion

        //TAB2
        #region tab2
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lista_getExVal.Clear();
            richTextBox2.Text = string.Empty;
            checkedListBox1.Items.Clear();
            
            switch (comboBox2.SelectedItem.ToString())
                {
                    case "CONNECTIVITYNODE":
                        lista_getExVal = gda.GetExtentValues(ModelCode.CONNECTIVITYNODE);
                        break;
                    case "TERMINAL":
                        lista_getExVal = gda.GetExtentValues(ModelCode.TERMINAL);
                        break;
                    case "POWERTR":
                        lista_getExVal = gda.GetExtentValues(ModelCode.POWERTR);
                        break;
                    case "POWTREND":
                        lista_getExVal = gda.GetExtentValues(ModelCode.POWTREND);
                        break;
                    case "RATIOTAPCHANGER":
                        lista_getExVal = gda.GetExtentValues(ModelCode.RATIOTAPCHANGER);
                        break;
            }

            pomocna.Clear();

            foreach (var v in lista_getExVal) 
            {
                short type = ModelCodeHelper.ExtractTypeFromGlobalId(v);
                properties = modelRD.GetAllPropertyIds((DMSType)type);
                listaString.Clear();                
                foreach (ModelCode p in properties)
                {
                     listaString.Add(p.ToString());                   
                }
                pomocna.Add(v);
            }
            
            distinct = listaString.Distinct().ToList();
            foreach (string d in distinct)
            {
                checkedListBox1.Items.Add(d);
            }

            checkedListBox1.Enabled = true;
            button4.Enabled = true;

        }
        

        //klik na dugme kod Get Extended Values
        private void button1_Click(object sender, EventArgs e)
        {
            ispis1.Text = string.Empty;
            tekst = "";
            List<string> listStr = new List<string>();
            List<string> listTip = new List<string>();
            if (checkedListBox1.SelectedItem != null)
            {
                for (int temp = 0; temp < checkedListBox1.CheckedItems.Count; temp++)
                {
                    try
                    {

                        string tip = (string)checkedListBox1.CheckedItems[temp];
                        listTip.Add(tip);
                        ModelCode tip2 = modelRD.GetModelCodeFromModelCodeName(tip);
                        List<ResourceDescription> rd = new List<ResourceDescription>();
                        foreach (long mc in pomocna)
                        {
                            rd.Add(gda.GetValues(mc));

                        }
                        
                        foreach (ResourceDescription r in rd)
                        {
                            for (int i = 0; i < r.Properties.Count(); i++)
                            {
                                if (r.Properties[i].Id == tip2)
                                {
                                    if (r.Properties[i].PropertyValue.LongValues.Count != 0)
                                    {
                                        for (int k = 0; k < r.Properties[i].PropertyValue.LongValues.Count; k++)
                                        {
                                            listStr.Add(r.Properties[i].PropertyValue.LongValues[k].ToString());
                                        }
                                    }
                                    else
                                    {
                                        listStr.Add(r.Properties[i].GetValue().ToString());
                                    }
                                }
                            }
                        }
                        rd.Clear();
                    }
                    catch
                    {
                        ispis1.Text = "ERROR";
                    }
                }

                int br = listStr.Count() / checkedListBox1.CheckedItems.Count;
                for (int i = 0; i < br; i++)
                {
                    ispis1.Text += "\n" + comboBox2.SelectedItem.ToString() + (i + 1) + ":\n";
                    for(int u = 0; u < checkedListBox1.CheckedItems.Count; u++)
                    {
                        ispis1.Text += listTip[u] +": " + listStr[br*u + i] + "\n";
                    }
                }
                
            }

            listaString.Clear();
            distinct.Clear();

        }

 
        private void button4_Click(object sender, EventArgs e)
        {
            lista_getExVal.Clear();
            ispis1.Text = string.Empty;

            try
            {
                switch (comboBox2.SelectedItem.ToString())
                {
                    case "CONNECTIVITYNODE":
                        lista_getExVal = gda.GetExtentValues(ModelCode.CONNECTIVITYNODE);
                        break;
                    case "TERMINAL":
                        lista_getExVal = gda.GetExtentValues(ModelCode.TERMINAL);
                        break;
                    case "POWERTR":
                        lista_getExVal = gda.GetExtentValues(ModelCode.POWERTR);
                        break;
                    case "POWTREND":
                        lista_getExVal = gda.GetExtentValues(ModelCode.POWTREND);
                        break;
                    case "RATIOTAPCHANGER":
                        lista_getExVal = gda.GetExtentValues(ModelCode.RATIOTAPCHANGER);
                        break;

                }

                foreach (long gid in lista_getExVal)
                {
                    ResourceDescription rd = gda.GetValues(gid);
                    ispis1.Text += gda.IspisUTextBox(rd);
                }

            }
            catch
            {
                ispis1.Text = "ERROR!";
            }
        }

        #endregion

        //TAB3
        #region tab3
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            lista_getRelVal.Clear();
            richTextBox2.Text = string.Empty;
            comboBox5.Items.Clear();

            switch (comboBox4.SelectedItem.ToString())
            {
                case "CONNECTIVITYNODE":
                    lista_getRelVal = gda.GetExtentValues(ModelCode.CONNECTIVITYNODE);
                    break;
                case "TERMINAL":
                    lista_getRelVal = gda.GetExtentValues(ModelCode.TERMINAL);
                    break;
                case "POWERTR":
                    lista_getRelVal = gda.GetExtentValues(ModelCode.POWERTR);
                    break;
                case "POWTREND":
                    lista_getRelVal = gda.GetExtentValues(ModelCode.POWTREND);
                    break;
                case "RATIOTAPCHANGER":
                    lista_getRelVal = gda.GetExtentValues(ModelCode.RATIOTAPCHANGER);
                    break;
            }

            foreach (var v in lista_getRelVal)
            {
                comboBox5.Items.Add(v);
            }
            comboBox5.SelectedIndex = 0;
            comboBox5.Enabled = true;
        }

        //popunjavanje 3. comboboxa iz 3. taba
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            listaPropertija.Clear();
            richTextBox2.Text = string.Empty;
            comboBox6.Items.Clear();

            try
            {
                long gid = (long)comboBox5.SelectedItem;

                ModelCode mc = modelRD.GetModelCodeFromId(gid);
                ResourceDescription rd = gda.GetValues(gid);


                for (int i = 0; i < rd.Properties.Count; i++)
                {
                    switch (rd.Properties[i].Type)
                    {
                        case PropertyType.Reference:
                            listaPropertija.Add(rd.Properties[i].Id); break;
                        case PropertyType.ReferenceVector:
                            listaPropertija.Add(rd.Properties[i].Id); break;
                    }
                }

                foreach (var v in listaPropertija)
                {
                    comboBox6.Items.Add(v);
                }
            }
            catch
            {
                richTextBox2.Text = "ERROR!";
            }

            if (comboBox5.SelectedItem != null)
            {
                comboBox6.Enabled = true;
                comboBox6.SelectedIndex = 0;
            }
            else
            {
                comboBox6.Enabled = false;
            }

        }

        //omogucen rad dugmeta i punjenje Combobox-a 7
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox7.Items.Clear();

            long gid = (long)comboBox5.SelectedItem;
            ModelCode propertyId = (ModelCode)comboBox6.SelectedItem;
            ModelCode type;

            ResourceDescription rd = gda.GetValues(gid);
            List<long> gids = new List<long>();
            for (int i = 0; i < rd.Properties.Count; i++)
            {
                if (rd.Properties[i].Id == propertyId)
                {
                    for(int k = 0; k < rd.Properties[i].PropertyValue.LongValues.Count();k++)
                    {
                        gids.Add(rd.Properties[i].PropertyValue.LongValues[k]);
                    }                    
                }
            }

            for (int i = 0; i < gids.Count; i++)
            {
                try
                {
                    type = modelRD.GetModelCodeFromId(gids[i]);
                    comboBox7.Items.Add(type);
                }
                catch
                {
                    continue;
                }
            }
            comboBox7.SelectedIndex = 0;
            comboBox7.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = string.Empty;
            tekst = "";
            List<string> listStr = new List<string>();
            List<string> listTip = new List<string>();
            if (checkedListBox2.SelectedItem != null)
            {
                for (int temp = 0; temp < checkedListBox2.CheckedItems.Count; temp++)
                {
                    try
                    {

                        string tip = (string)checkedListBox2.CheckedItems[temp];
                        listTip.Add(tip);
                        ModelCode tip2 = modelRD.GetModelCodeFromModelCodeName(tip);
                        List<ResourceDescription> rd = new List<ResourceDescription>();
                        foreach (long mc in pomocna)
                        {
                            rd.Add(gda.GetValues(mc));

                        }

                        foreach (ResourceDescription r in rd)
                        {
                            for (int i = 0; i < r.Properties.Count(); i++)
                            {
                                if (r.Properties[i].Id == tip2)
                                {
                                    if (r.Properties[i].PropertyValue.LongValues.Count != 0)
                                    {
                                        for (int k = 0; k < r.Properties[i].PropertyValue.LongValues.Count; k++)
                                        {
                                            listStr.Add(r.Properties[i].PropertyValue.LongValues[k].ToString());
                                        }
                                    }
                                    else
                                    {
                                        listStr.Add(r.Properties[i].GetValue().ToString());
                                    }

                                }

                            }

                        }
                        rd.Clear();
                    }
                    catch
                    {
                        richTextBox2.Text = "ERROR";
                    }                  

                }

                int br = listStr.Count() / checkedListBox2.CheckedItems.Count;
                for (int i = 0; i < br; i++)
                {
                    richTextBox2.Text += "\n" + comboBox7.SelectedItem.ToString() + (i + 1) + ":\n";
                    for (int u = 0; u < checkedListBox2.CheckedItems.Count; u++)
                    {
                        richTextBox2.Text += listTip[u] + ": " + listStr[br * u + i] + "\n";
                    }
                }

            }

            listaString.Clear();
            distinct.Clear();
        }


        #endregion

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedItem != null)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox2.SelectedItem != null)
                button3.Enabled = true;
            else
                button3.Enabled = false;
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            lista_getExVal.Clear();
            richTextBox2.Text = string.Empty;
            checkedListBox2.Items.Clear();

            switch (comboBox7.SelectedItem.ToString())
            {
                case "CONNECTIVITYNODE":
                    lista_getExVal = gda.GetExtentValues(ModelCode.CONNECTIVITYNODE);
                    break;
                case "TERMINAL":
                    lista_getExVal = gda.GetExtentValues(ModelCode.TERMINAL);
                    break;
                case "POWERTR":
                    lista_getExVal = gda.GetExtentValues(ModelCode.POWERTR);
                    break;
                case "POWTREND":
                    lista_getExVal = gda.GetExtentValues(ModelCode.POWTREND);
                    break;
                case "RATIOTAPCHANGER":
                    lista_getExVal = gda.GetExtentValues(ModelCode.RATIOTAPCHANGER);
                    break;
            }

            pomocna.Clear();

            foreach (var v in lista_getExVal)
            {
                short type = ModelCodeHelper.ExtractTypeFromGlobalId(v);
                properties = modelRD.GetAllPropertyIds((DMSType)type);
                listaString.Clear();
                foreach (ModelCode p in properties)
                {
                    listaString.Add(p.ToString());
                }
                pomocna.Add(v);
            }

            distinct = listaString.Distinct().ToList();
            foreach (string d in distinct)
            {
                checkedListBox2.Items.Add(d);
            }
            checkedListBox2.Enabled = true;
        }
    }
}
