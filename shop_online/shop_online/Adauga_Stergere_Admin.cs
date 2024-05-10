﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace shop_online
{
    public partial class Adauga_Stergere_Admin : Form
    {
        bool Adauga = true;
        public Adauga_Stergere_Admin()
        {
            InitializeComponent();
        }
        public Adauga_Stergere_Admin( bool adauga )
        {
            InitializeComponent();
            Adauga = adauga;
        }

        private void Adauga_Stergere_Admin_FormClosed( object sender, FormClosedEventArgs e )
        {
            if (Application.OpenForms ["Afisare_Produse"] != null)
                Application.OpenForms ["Afisare_Produse"].Show();
        }
        private void Adauga_Stergere_Admin_Load( object sender, EventArgs e )
        {
            buttonAdauga_Sterge.Text = Adauga ? "Adauga" : "Sterge";

            string connectionString = null;
            try
            {
                connectionString = Aranjare.GetConnectionString();
            }
            catch (Exception) { MessageBox.Show("Eroare la obținerea șirului de conexiune."); return; }
            if (Adauga)
            {
                List<string> emails = Interogari.GetAllUserEmails(connectionString);
                if (emails != null)
                {
                    comboBoxEmail.Items.AddRange(emails.ToArray());
                }
            }
            else
            {
                List<string> emails = Interogari.GetAdminUserEmails(connectionString);
                if (emails != null)
                {
                    comboBoxEmail.Items.AddRange(emails.ToArray());
                }
            }

            List<string> roles = Interogari.GetAllAdminRoles(connectionString);
            if (roles != null)
            {
                comboBoxRol.Items.AddRange(roles.ToArray());
            }
        }

        private void buttonAdauga_Sterge_Click( object sender, EventArgs e )
        {
            string email = comboBoxEmail.Text;
            string rol = comboBoxRol.Text;
            string connectionString = null;
            try
            {
                connectionString = Aranjare.GetConnectionString();
            }
            catch (Exception) { MessageBox.Show("Eroare la obținerea șirului de conexiune."); return; }

            int iduser = Interogari.GetUserIDbyEmail(connectionString, email);

            if (!Adauga)
            {
                if (Interogari.DeleteAdmin(connectionString, iduser))
                    MessageBox.Show("Adminul a fost șters cu succes.");
                else
                    MessageBox.Show("Eroare la ștergerea administratorului.");

            }

            if (Interogari.CheckIfAdminExists(connectionString, iduser))
            {
                MessageBox.Show("Adminul exista deja");
                return;
            }

            if (Interogari.InsertAdmin(connectionString, iduser, rol))
                MessageBox.Show("Noul admin a fost adăugat cu succes.");
            else
                MessageBox.Show("Eroare la adăugarea administratorului.");
        }
    }
}
