﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ysfo.App
{
    public partial class MainForm : Form
    {
        YsfoWrapper _ysfo;

        public MainForm()
        {
            InitializeComponent();
        }

        #region Aircraft

        private void lbxAircraftUnloaded_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxAircraftUnloaded.SelectedItem != null)
            {
                lbxAircraftLoaded.ClearSelected();

                // buttons
                btnAircraftLoad.Enabled = true;
                btnAircraftUnload.Enabled = false;
            }
        }

        private void lbxAircraftLoaded_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxAircraftLoaded.SelectedItem != null)
            {
                lbxAircraftUnloaded.ClearSelected();

                // buttons
                btnAircraftLoad.Enabled = false;
                btnAircraftUnload.Enabled = true;
            }
        }

        private void btnAircraftUp_Click(object sender, EventArgs e)
        {
            // loaded
            Ysfo.Core.AircraftAddon aircraft = (Ysfo.Core.AircraftAddon)lbxAircraftLoaded.SelectedItem;

            if (aircraft != null)
            {
                if (_ysfo.LoadedAircraft.MoveItem(aircraft, Extensions.MoveDirection.Up))
                {
                    lbxAircraftLoaded.SelectedIndex -= 1;
                }
            }

            // unloaded
            aircraft = (Ysfo.Core.AircraftAddon)lbxAircraftUnloaded.SelectedItem;

            if (aircraft != null)
            {
                if (_ysfo.UnloadedAircraft.MoveItem(aircraft, Extensions.MoveDirection.Up))
                {
                    lbxAircraftUnloaded.SelectedIndex -= 1;
                }
            }
        }

        private void btnAircraftDown_Click(object sender, EventArgs e)
        {
            // loaded
            Ysfo.Core.AircraftAddon aircraft = (Ysfo.Core.AircraftAddon)lbxAircraftLoaded.SelectedItem;

            if (aircraft != null)
            {
                if (_ysfo.LoadedAircraft.MoveItem(aircraft, Extensions.MoveDirection.Down))
                {
                    lbxAircraftLoaded.SelectedIndex += 1;
                }
            }

            // unloaded
            aircraft = (Ysfo.Core.AircraftAddon)lbxAircraftUnloaded.SelectedItem;

            if (aircraft != null)
            {
                if (_ysfo.UnloadedAircraft.MoveItem(aircraft, Extensions.MoveDirection.Down))
                {
                    lbxAircraftUnloaded.SelectedIndex += 1;
                }
            }
        }

        #endregion

        #region Settings

        private void btnSettingsBrowse_Click(object sender, EventArgs e)
        {
            // set path
            diaSettingsBrowseYsPath.SelectedPath = _ysfo.Path;

            // show dialog
            DialogResult result = diaSettingsBrowseYsPath.ShowDialog();

            if (result == DialogResult.OK)
            {
                // update textbox
                _ysfo.Path = diaSettingsBrowseYsPath.SelectedPath;
            }
        }

        private void btnSettingsSave_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        #endregion

        #region FormEvents

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _ysfo.Dispose();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // initialise
            _ysfo = new YsfoWrapper();

            // settings
            tbxSettingsPath.DataBindings.Add("Text", _ysfo, "Path");
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            LoadData();
        }

        private void mitExit_Click(object sender, EventArgs e)
        {
            // close form
            Close();
        }

        #endregion

        private void LoadData()
        {
            try
            {
                _ysfo.Setup();

                // enable tabs
                tpgAircraft.Enabled = true;
                tpgObjects.Enabled = true;
                tpgMaps.Enabled = true;

                // reset bindings
                lbxAircraftLoaded.DataSource = new BindingSource(_ysfo, "LoadedAircraft");
                lbxAircraftLoaded.ClearSelected();
                lbxAircraftUnloaded.DataSource = new BindingSource(_ysfo, "UnloadedAircraft");
                lbxAircraftUnloaded.ClearSelected();
            }
            catch (YsfoWrapper.YsfoPathInvalidException)
            {
                // change tabs
                tpgAircraft.Enabled = false;
                tpgObjects.Enabled = false;
                tpgMaps.Enabled = false;
                tabControl.SelectedTab = tpgSettings;

                MessageBox.Show("The path to your YsFlight directory is invalid!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}
