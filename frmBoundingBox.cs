using Microsoft.VisualBasic.Logging;
using System.Diagnostics;
using System.Timers;

namespace BoundingBox
{
    public partial class frmBoundingBox : Form
    {
        public frmBoundingBox()
        {
            InitializeComponent();
        }

        PointF getRotatedLocation(float Angle, PointF Center, PointF Pos)
        {
            float fX = (float)Math.Cos(Angle)*(Pos.X - Center.X) - (float)Math.Sin(Angle)*(Pos.Y - Center.Y) + Center.X;
            float fY = (float)Math.Sin(Angle)*(Pos.X - Center.X) + (float)Math.Cos(Angle)*(Pos.Y - Center.Y) + Center.Y;

            return new PointF(fX, fY);
        }

        void drawRotatedRectangle(Graphics Drawer, float fAngle, int Width, int Height, PointF Center = new PointF())
        {
            if (Drawer == null)
            {
                return;
            }

            PointF tL = getRotatedLocation(fAngle, new PointF((float)Width / 2.0f, (float)Height / 2.0f), new PointF(0.0f, 0.0f));
            PointF tR = getRotatedLocation(fAngle, new PointF((float)Width / 2.0f, (float)Height / 2.0f), new PointF((float)Width, 0.0f));
            PointF bL = getRotatedLocation(fAngle, new PointF((float)Width / 2.0f, (float)Height / 2.0f), new PointF(0.0f,(float)Height));
            PointF bR = getRotatedLocation(fAngle, new PointF((float)Width / 2.0f, (float)Height / 2.0f), new PointF((float)Width,(float)Height));

            //if (fAngle > 5.41 && fAngle < 5.42)
                //Debug.Write(String.Format("Rectangle: TL ({0},{1}) TR ({2},{3}) BL ({4},{5}) BR ({6},{7})", tL.X, tL.Y,tR.X, tR.Y,bL.X, bL.Y,bR.X, bR.Y));

            float maxY = Math.Max(tL.Y,Math.Max(tR.Y, Math.Max( bL.Y, bR.Y)));
            float minY = Math.Min(tL.Y, Math.Min(tR.Y, Math.Min(bL.Y, bR.Y)));
            float maxX = Math.Max(tL.X, Math.Max(tR.X, Math.Max(bL.X, bR.X)));
            float minX = Math.Min(tL.X, Math.Min(tR.X, Math.Min(bL.X, bR.X)));

            //if (fAngle > 5.41 && fAngle < 5.42)
                //Debug.Write(String.Format("Width: {0} Height: {1}\r\n", (float)maxX - minX, (float)maxY - minY));

            Pen pBlack = new Pen(Color.FromArgb(255, 255, 0, 0));
            pBlack.Width = 10;

            Drawer.TranslateTransform(Center.X - Width/2.0f, Center.Y - Height/2.0f);

            Drawer.DrawLine(pBlack, tL, tR);
            Drawer.DrawLine(pBlack, tL, bL);
            Drawer.DrawLine(pBlack, bL, bR);
            Drawer.DrawLine(pBlack, tR, bR);
        }

        private void drawRectangleAndBoundingBox(float Rotation)
        {
            const int width = 337;
            const int height = 262;
            Rectangle box = new Rectangle(0, 0, width, height);

            int bwidth = (int)(Math.Abs(width * Math.Cos(Rotation)) + Math.Abs(height * Math.Sin(Rotation)));
            int bheight = (int)(Math.Abs(height * Math.Cos(Rotation)) + Math.Abs(width * Math.Sin(Rotation)));
            
            float fCenterBoundX = (float)300.0f - (float)bwidth/2.0f;
            float fCenterBoundY = (float)300.0f - (float)bheight / 2.0f;
            
            Rectangle bounding = new Rectangle(0, 0, bwidth, bheight);

            Graphics gDrawer = picBoundingBox.CreateGraphics();

            gDrawer.Clear(Color.White); 
            Pen pRed = new Pen(Color.FromArgb(70, 255, 0, 0));
            Pen pBlack = new Pen(Color.FromArgb(255, 255, 0, 0));

            pRed.Width = 5;
            pBlack.Width = 10;


            gDrawer.TranslateTransform(300.0f - width/2.0f, 300.0f - height/2.0f);
            gDrawer.DrawEllipse(pRed, width / 2.0f, height / 2.0f, 10, 10);
            gDrawer.ResetTransform();

            gDrawer.TranslateTransform(fCenterBoundX, fCenterBoundY);
            gDrawer.DrawRectangle(pRed, bounding);
            gDrawer.ResetTransform();

            drawRotatedRectangle(gDrawer, Rotation, width, height, new PointF(300.0f,300.0f));


            gDrawer.Flush();
            gDrawer.Dispose();
        }

        float fAngle = 0.0f;
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            fAngle += (float)(Math.PI/180.0f);

            if (fAngle >= (float)(360 * (Math.PI / 180.0f)))
            {
                fAngle = 0.0f;
            }

            //if ( fAngle > 5.41 && fAngle < 5.42)
                //Debug.WriteLine(string.Format("Rotation: {0} Radians: {1}", fAngle * (180.0f / Math.PI), fAngle));

            drawRectangleAndBoundingBox(fAngle);
        }
        private void frmBoundingBox_Load(object sender, EventArgs e)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 100; // ~ .5 seconds
            aTimer.Enabled = true;
        }
    }
}
