using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System.Media;
using System.Speech;
using System.Speech.Synthesis;

namespace SpeakerMicTest
{
    public partial class Form1 : Form
    {
        NAudio.Wave.WaveIn sourceStrem = null;
        NAudio.Wave.WaveIn sourceStrem1 = null;
        NAudio.CoreAudioApi.MMDevice firstMic = null;
        NAudio.CoreAudioApi.MMDevice firstSpkrs = null;
        int i, j, k, l, m, Speakers, Microphone, MicType, SpkrsType, SpkrsGain = 0, MicGain = 0;
        SoundPlayer SpeakersSound;
        SoundPlayer MicSound;
        SpeechSynthesizer TalkBk = new SpeechSynthesizer();
        public Form1()
        {
            InitializeComponent();
            progressBar1.Maximum = 100;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            i = 1;
            j = 1;
            k = 1;
            l = 1;
            m = 1;
            Speakers = 1;
            Microphone = 1;
            TestStart.Start();
            progressBar1.Value = 0;
            label12.Text = "";
            label15.Text = "";
            label9.Text = "           Initializing   ●●●●●●";
            label10.Text = "           Initializing   ●●●●●●";
            this.pictureBox3.Image = Properties.Resources.lvl0;
            this.pictureBox4.Image = Properties.Resources.lvl0;
            richTextBox1.Text = "Logs.....>>";
            SpkrsGain = 0;
            MicGain = 0;
        }

        private void TestStart_Tick(object sender, EventArgs e)
        {
            switch (i)
            {
                case 1:
                    {
                        label7.Text = "Initializing.";
                        i++;
                        break;
                    }
                case 2:
                    {
                        label7.Text = "Initializing..";
                        i++;
                        break;
                    }
                case 3:
                    {
                        label7.Text = "Initializing...";
                        i++;
                        break;
                    }
                case 4:
                    {
                        label7.Text = "Initializing....";
                        i++;
                        break;
                    }
                case 5:
                    {
                        label7.Text = "Initializing.....";
                        i++;
                        break;
                    }
                case 6:
                    {
                        label7.Text = "Testing now";
                        i=0;
                        TestStart.Stop();
                        TotalEventTime.Start();
                        TestOngoing();
                        break;
                    }
            }
        }

        private void TestOngoing()
        {
            //Testing Speakers.........................
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            richTextBox1.Text += System.Environment.NewLine + "Speakers test started:";
            InitializingSpeakers.Start();
        }

        private void TotalEventTime_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(10);
            if (j != 10)
            {
                int x = j * 10;
                label12.Text = x.ToString() + "%";
                j++;
            }
            else if (j == 10)
            {
                label12.Text ="100%";
                TotalEventTime.Stop();
                richTextBox1.Text += System.Environment.NewLine + "Publishing result!";
                Finalizing.Start();
            }
            
        }

        private void Finalizing_Tick(object sender, EventArgs e)
        {
            switch (k)
            {
                case 1:
                    {
                        label15.Text = "●";
                        k++;
                        break;
                    }
                case 2:
                    {
                        label15.Text = "●●";
                        k++;
                        break;
                    }
                case 3:
                    {
                        label15.Text = "●●●";
                        k++;
                        break;
                    }
                case 4:
                    {
                        label15.Text = "●●●●";
                        k++;
                        break;
                    }
                case 5:
                    {
                        label15.Text = "●●●●●";
                        k++;
                        break;
                    }
                case 6:
                    {
                        if (SpkrsGain > 0 && MicGain > 0)
                        {
                            label15.Text = "Test passed!";
                        }
                        else if (SpkrsGain <= 0 && MicGain > 0)
                        {
                            label15.Text = "Test faild(Spk)!";
                        }
                        else if (SpkrsGain > 0 && MicGain <= 0)
                        {
                            label15.Text = "Test faild(Mic)!";
                        }
                        else if (SpkrsGain <= 0 && MicGain <= 0)
                        {
                            label15.Text = "Test faild!";
                        }
                        k = 0;
                        Finalizing.Stop();
                        label7.Text = "Self Test.......";
                        break;
                    }
            }
        }

        private void InitializingSpeakers_Tick(object sender, EventArgs e)
        {
            switch (l)
            {
                case 1:
                    {
                        label9.Text = "           Initializing   ●";
                        l++;
                        break;
                    }
                case 2:
                    {
                        label9.Text = "           Initializing   ●●";
                        l++;
                        break;
                    }
                case 3:
                    {
                        label9.Text = "           Initializing   ●●●";
                        l++;
                        break;
                    }
                case 4:
                    {
                        label9.Text = "           Initializing   ●●●●";
                        l++;
                        break;
                    }
                case 5:
                    {
                        label9.Text = "           Initializing   ●●●●●";
                        l++;
                        break;
                    }
                case 6:
                    {
                        label9.Text = "           Initializing   ●●●●●●";
                        l++;
                        break;
                    }
                case 7:
                    {
                        InitializingSpeakers.Stop();
                        SpeakersTesting();
                        if (SpkrsType == 0)
                        {
                            label9.Text = "              Internal Speakers";
                            richTextBox1.Text += System.Environment.NewLine + "internal Speakers detected";
                        }
                        else if (SpkrsType == 1)
                        {
                            label9.Text = "              External Speakers";
                            richTextBox1.Text += System.Environment.NewLine + "external Speakers detected";
                        }
                        this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.pictureBox1.Image = Properties.Resources.Activated;
                        TalkBk.SpeakAsync("Testing Speakers");
                        TestSpeakers();
                        l = 0;
                        break;
                    }
            }
        }

        private void SpeakersTesting()
        {
            sourceStrem1 = new NAudio.Wave.WaveIn();
            sourceStrem1.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(0).Channels);
            sourceStrem1.StartRecording();
            sourceStrem1.DataAvailable += new EventHandler<WaveInEventArgs>(sourceStrem1_DataAvailable);
            MMDeviceEnumerator mde = new MMDeviceEnumerator();
            var devices = mde.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            if (devices.Count == 1)
            {
                SpkrsType = 0;
                firstSpkrs = devices.ElementAt(SpkrsType);
            }
            else if (devices.Count == 2)
            {
                SpkrsType = 1;
                firstSpkrs = devices.ElementAt(SpkrsType);
            }
        }

        void sourceStrem1_DataAvailable(object sender, WaveInEventArgs e)
        {
            int value;
            value = (int)(Math.Round(firstSpkrs.AudioMeterInformation.MasterPeakValue * 10));
            
            switch (value)
            {
                case 0:
                    //SpkrsGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl0;
                    this.pictureBox4.Image = Properties.Resources.lvl0;
                    break;
                case 1:
                    SpkrsGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl1;
                    this.pictureBox4.Image = Properties.Resources.lvl1;
                    break;
                case 2:
                    SpkrsGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl2;
                    this.pictureBox4.Image = Properties.Resources.lvl2;
                    break;
                case 3:
                    SpkrsGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl3;
                    this.pictureBox4.Image = Properties.Resources.lvl3;
                    break;
                case 4:
                    SpkrsGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl4;
                    this.pictureBox4.Image = Properties.Resources.lvl4;
                    break;
                case 5:
                    SpkrsGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl5;
                    this.pictureBox4.Image = Properties.Resources.lvl5;
                    break;
                case 6:
                    SpkrsGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl6;
                    this.pictureBox4.Image = Properties.Resources.lvl6;
                    break;
                case 7:
                    SpkrsGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl7;
                    this.pictureBox4.Image = Properties.Resources.lvl7;
                    break;
                case 8:
                    SpkrsGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl8;
                    this.pictureBox4.Image = Properties.Resources.lvl8;
                    break;
                case 9:
                    SpkrsGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl9;
                    this.pictureBox4.Image = Properties.Resources.lvl9;
                    break;
                case 10:
                    SpkrsGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl10;
                    this.pictureBox4.Image = Properties.Resources.lvl10;
                    break;
            }
        }

        private void TestSpeakers()
        {
            TestingSpeakers.Start();
        }

        private void TestingSpeakers_Tick(object sender, EventArgs e)
        {
            
            switch (Speakers)
            {
                case 1:
                    {
                        this.LSp.Image = Properties.Resources.LeftSpkr0;
                        this.RSp.Image = Properties.Resources.RightSpkr0;
                        Speakers++;
                        break;
                    }
                case 2:
                    {
                        this.LSp.Image = Properties.Resources.LeftSpkr1;
                        this.RSp.Image = Properties.Resources.RightSpkr1;
                        Speakers++;
                        break;
                    }
                case 3:
                    {
                        this.LSp.Image = Properties.Resources.LeftSpkr2;
                        this.RSp.Image = Properties.Resources.RightSpkr2;
                        Speakers++;
                        break;
                    }
                case 4:
                    {
                        SpeakersSound = new SoundPlayer("SpeakersTest.wav");
                        SpeakersSound.Play();
                        this.LSp.Image = Properties.Resources.LeftSpkr3;
                        this.RSp.Image = Properties.Resources.RightSpkr3;
                        Speakers++;
                        break;
                    }
                case 5:
                    {
                        this.LSp.Image = Properties.Resources.LeftSpkr0;
                        this.RSp.Image = Properties.Resources.RightSpkr0;
                        Speakers++;
                        break;
                    }
                case 6:
                    {
                        this.LSp.Image = Properties.Resources.LeftSpkr1;
                        this.RSp.Image = Properties.Resources.RightSpkr1;
                        Speakers++;
                        break;
                    }
                case 7:
                    {
                        this.LSp.Image = Properties.Resources.LeftSpkr2;
                        this.RSp.Image = Properties.Resources.RightSpkr2;
                        Speakers++;
                        break;
                    }
                case 8:
                    {
                        this.LSp.Image = Properties.Resources.LeftSpkr3;
                        this.RSp.Image = Properties.Resources.RightSpkr3;
                        Speakers++;
                        break;
                    }
                case 9:
                    {
                        sourceStrem1.StopRecording();
                        this.LSp.Image = Properties.Resources.LeftSpkr0;
                        this.RSp.Image = Properties.Resources.RightSpkr0;
                        Speakers = 0;
                        this.pictureBox1.Image = Properties.Resources.DeActivated;
                        TestingSpeakers.Stop();
                        this.pictureBox3.Image = Properties.Resources.lvl0;
                        this.pictureBox4.Image = Properties.Resources.lvl0;
                        if (SpkrsGain <= 0)
                        {
                            richTextBox1.Text += System.Environment.NewLine + "no output";
                        }
                        else if (SpkrsGain > 0)
                        {
                            richTextBox1.Text += System.Environment.NewLine + "output  ok!";
                        }
                        this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        richTextBox1.Text += System.Environment.NewLine + "Microphone test started:";
                        InitializingMic.Start();
                        break;
                    }
            }
        }

        private void TestingMic_Tick(object sender, EventArgs e)
        {
            switch (Microphone)
            {
                case 1:
                    {
                        this.Mic.Image = Properties.Resources.Mic0;
                        Microphone++;
                        break;
                    }
                case 2:
                    {
                        this.Mic.Image = Properties.Resources.Mic1;
                        Microphone++;
                        break;
                    }
                case 3:
                    {
                        this.Mic.Image = Properties.Resources.Mic0;
                        Microphone++;
                        break;
                    }
                case 4:
                    {
                        this.Mic.Image = Properties.Resources.Mic1;
                        Microphone++;
                        break;
                    }
                case 5:
                    {
                        MicSound = new SoundPlayer("MicTest.wav");
                        MicSound.Play();
                        this.Mic.Image = Properties.Resources.Mic0;
                        Microphone++;
                        break;
                    }
                case 6:
                    {
                        this.Mic.Image = Properties.Resources.Mic1;
                        Microphone++;
                        break;
                    }
                case 7:
                    {
                        this.Mic.Image = Properties.Resources.Mic0;
                        Microphone++;
                        break;
                    }
                case 8:
                    {
                        this.Mic.Image = Properties.Resources.Mic1;
                        Microphone++;
                        break;
                    }
                case 9:
                    {
                        sourceStrem.StopRecording();
                        this.Mic.Image = Properties.Resources.Mic0;
                        Microphone = 0;
                        this.pictureBox2.Image = Properties.Resources.DeActivated;
                        TestingMic.Stop();
                        this.pictureBox3.Image = Properties.Resources.lvl0;
                        this.pictureBox4.Image = Properties.Resources.lvl0;
                        if (MicGain <= 0)
                        {
                            richTextBox1.Text += System.Environment.NewLine + "no input";
                        }
                        else if (MicGain > 0)
                        {
                            richTextBox1.Text += System.Environment.NewLine + "input  ok!";
                        }
                        break;
                    }
            }
        }

        private void InitializingMic_Tick(object sender, EventArgs e)
        {
            switch (m)
            {
                case 1:
                    {
                        m++;
                        break;
                    }
                case 2:
                    {
                        m++;
                        break;
                    }
                case 3:
                    {
                        label10.Text = "           Initializing   ●";
                        m++;
                        break;
                    }
                case 4:
                    {
                        label10.Text = "           Initializing   ●●";
                        m++;
                        break;
                    }
                case 5:
                    {
                        label10.Text = "           Initializing   ●●●";
                        m++;
                        break;
                    }
                case 6:
                    {
                        label10.Text = "           Initializing   ●●●●";
                        m++;
                        break;
                    }
                case 7:
                    {
                        label10.Text = "           Initializing   ●●●●●";
                        m++;
                        break;
                    }
                case 8:
                    {
                        label10.Text = "           Initializing   ●●●●●●";
                        m++;
                        break;
                    }
                case 9:
                    {
                        InitializingMic.Stop();
                        MicrophoneTesting();
                        if (MicType == 0)
                        {
                            label10.Text = "              Internal Microphone";
                            richTextBox1.Text += System.Environment.NewLine + "internal microphone detected";
                        }
                        else if (MicType == 1)
                        {
                            label10.Text = "              External Microphone";
                            richTextBox1.Text += System.Environment.NewLine + "external microphone detected";
                        }
                        this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.pictureBox2.Image = Properties.Resources.Activated;
                        TalkBk.SpeakAsync("Testing Mic");
                        TestMicrophone();
                        m = 0;
                        break;
                    }
            }
        }

        private void MicrophoneTesting()
        {
            sourceStrem = new NAudio.Wave.WaveIn();
            sourceStrem.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(0).Channels);
            sourceStrem.StartRecording();
            sourceStrem.DataAvailable += new EventHandler<WaveInEventArgs>(sourceStrem_DataAvailable);
            MMDeviceEnumerator mde = new MMDeviceEnumerator();
            var devices = mde.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active);
            //label2.Text = devices.Count.ToString();
            if (devices.Count == 1)
            {
                MicType = 0;
                firstMic = devices.ElementAt(MicType);
            }
            else if (devices.Count == 2)
            {
                MicType = 1;
                firstMic = devices.ElementAt(MicType);
            }
        }

        void sourceStrem_DataAvailable(object sender, WaveInEventArgs e)
        {
            int value;
            value = (int)(Math.Round(firstMic.AudioMeterInformation.MasterPeakValue * 130));
            switch (value)
            {
                case 0:
                    this.pictureBox3.Image = Properties.Resources.lvl0;
                    this.pictureBox4.Image = Properties.Resources.lvl0;
                    break;
                case 1:
                    MicGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl1;
                    this.pictureBox4.Image = Properties.Resources.lvl1;
                    break;
                case 2:
                    MicGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl2;
                    this.pictureBox4.Image = Properties.Resources.lvl2;
                    break;
                case 3:
                    MicGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl3;
                    this.pictureBox4.Image = Properties.Resources.lvl3;
                    break;
                case 4:
                    MicGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl4;
                    this.pictureBox4.Image = Properties.Resources.lvl4;
                    break;
                case 5:
                    MicGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl5;
                    this.pictureBox4.Image = Properties.Resources.lvl5;
                    break;
                case 6:
                    MicGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl6;
                    this.pictureBox4.Image = Properties.Resources.lvl6;
                    break;
                case 7:
                    MicGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl7;
                    this.pictureBox4.Image = Properties.Resources.lvl7;
                    break;
                case 8:
                    MicGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl8;
                    this.pictureBox4.Image = Properties.Resources.lvl8;
                    break;
                case 9:
                    MicGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl9;
                    this.pictureBox4.Image = Properties.Resources.lvl9;
                    break;
                case 10:
                    MicGain = value;
                    this.pictureBox3.Image = Properties.Resources.lvl10;
                    this.pictureBox4.Image = Properties.Resources.lvl10;
                    break;
            }
        }

        private void TestMicrophone()
        {
            TestingMic.Start();
        }
    }
}
