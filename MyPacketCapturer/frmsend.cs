﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPacketCapturer
{
    public partial class frmsend : Form
    {

        public static int instantiations = 0;
        

        public frmsend()
        {
            InitializeComponent();
            instantiations++;

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
            openFileDialog1.Title = "Open Captured Packets";
            openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName != "")
            {
                txtPacket.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text Files|*.txt|All Files|*.*";
            saveFileDialog1.Title = "Save Captured Packets";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                System.IO.File.WriteAllText(saveFileDialog1.FileName, txtPacket.Text);
            }
        }

        private void btnsend_Click(object sender, EventArgs e)
        {
            string stringBytes = "";

            foreach (string s in txtPacket.Lines){

                string[] noCommments = s.Split('#');
                string s1 = noCommments[0];
                stringBytes += Environment.NewLine;

            }

            string [] sBytes=stringBytes.Split(new string[]{"\n","\r\n", " "},StringSplitOptions.RemoveEmptyEntries);

            byte[] packet = new byte[sBytes.Length];
            int i=0;
                foreach(string s in sBytes) { packet[i] = Convert.ToByte(s, 16); i++; }

            try
            {
                frmCapture.device.SendPacket(packet);
            }
            catch (Exception exp)
            {

            }
        }

        private void frmsend_FormClosed(object sender, FormClosedEventArgs e)
        {
            instantiations--;
        }
    }
}
