using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Agenty
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                string strConn = @"Data Source=DESKTOP-HVD7F6Q\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=Agenty";
                SqlConnection Conn = new SqlConnection(strConn);
                try
                {
                    // Открываем подключение
                    Conn.Open();
                    MessageBox.Show(string.Format("Подключение открыто"));
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            string Title = textBox1.Text;
            object AgentTypeID = comboBox1.Text;
            string Address = textBox2.Text;
            int INN = Convert.ToInt32(textBox3.Text);
            int KPP = Convert.ToInt32(textBox4.Text);
            string DirectorName = textBox5.Text;
            string Phone = textBox6.Text;
            string Email = textBox7.Text;
            string Logo = textBox8.Text;
            int Priority = Convert.ToInt32(textBox9.Text);

                // Создаем и открываем соединение с MS SQL Server ...
                {
                    string query = "SELECT * FROM AgentType WHERE Title = '" + AgentTypeID + "'";

                    SqlCommand cmd = new SqlCommand(query, Conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            object ID = reader.GetValue(0);
                        AgentTypeID = ID;
                        }
                    }
                    reader.Close();
                    // Заготовка Sql-предложения Insert ...
                    string sInsSql = "Insert into Agent (Title, AgentTypeID,  Address, INN, KPP,  DirectorName, Phone, Email, Logo, Priority) Values('{0}', {1} , '{2}' , {3} , {4} , '{5}' , '{6}' , '{7}' , '{8}', {9})";
                    string sInsSotr = string.Format(sInsSql, Title, AgentTypeID, Address, INN, KPP, DirectorName, Phone, Email, Logo, Priority);
                    // Создаем команду ...
                    SqlCommand cmdIns = new SqlCommand(sInsSotr, Conn);
                    try
                    {
                        cmdIns.ExecuteNonQuery();
                        Conn.Close();
                        MessageBox.Show(string.Format("Запись добавлена"));
                    }
                    catch
                    {
                        MessageBox.Show(string.Format("Запись не добавлена"));
                    }
                }
            }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "dataSet2.AgentType". При необходимости она может быть перемещена или удалена.
            this.agentTypeTableAdapter.Fill(this.dataSet2.AgentType);

        }
    }
    }

