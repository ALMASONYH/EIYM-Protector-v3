
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MasonProtector.Core
{
    internal static class Design
    {
        public static void DrawOuterBorder(Panel panel, Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen darkBorder = new Pen(Color.FromArgb(0, 48, 99)))
                g.DrawRectangle(darkBorder, 0, 0, panel.Width - 1, panel.Height - 1);

            using (Pen midBorder = new Pen(Color.FromArgb(0, 60, 116)))
                g.DrawRectangle(midBorder, 1, 1, panel.Width - 3, panel.Height - 3);

            using (Pen lightEdge = new Pen(Color.FromArgb(80, 166, 202, 240)))
            {
                g.DrawLine(lightEdge, 2, 2, panel.Width - 4, 2);
                g.DrawLine(lightEdge, 2, 2, 2, panel.Height - 4);
            }
        }

        public static void DrawWindowFrame(Panel panel, Graphics g)
        {
            Rectangle leftRect = new Rectangle(0, 0, 4, panel.Height);
            using (LinearGradientBrush leftBrush = new LinearGradientBrush(leftRect, Color.FromArgb(66, 142, 255), Color.FromArgb(0, 60, 165), LinearGradientMode.Horizontal))
                g.FillRectangle(leftBrush, leftRect);

            Rectangle rightRect = new Rectangle(panel.Width - 4, 0, 4, panel.Height);
            using (LinearGradientBrush rightBrush = new LinearGradientBrush(rightRect, Color.FromArgb(0, 60, 165), Color.FromArgb(0, 48, 130), LinearGradientMode.Horizontal))
                g.FillRectangle(rightBrush, rightRect);

            Rectangle bottomRect = new Rectangle(0, panel.Height - 4, panel.Width, 4);
            using (LinearGradientBrush bottomBrush = new LinearGradientBrush(bottomRect, Color.FromArgb(0, 60, 165), Color.FromArgb(0, 48, 130), LinearGradientMode.Vertical))
                g.FillRectangle(bottomBrush, bottomRect);

            using (Pen innerLight = new Pen(Color.FromArgb(100, 120, 180, 255)))
                g.DrawLine(innerLight, 1, 0, 1, panel.Height - 2);

            using (Pen innerShadow = new Pen(Color.FromArgb(100, 0, 40, 100)))
            {
                g.DrawLine(innerShadow, panel.Width - 2, 0, panel.Width - 2, panel.Height - 2);
                g.DrawLine(innerShadow, 0, panel.Height - 2, panel.Width, panel.Height - 2);
            }
        }

        public static void DrawTitleBar(Panel panel, Graphics g)
        {
            Rectangle gradientRect = new Rectangle(0, 0, panel.Width, panel.Height);

            using (LinearGradientBrush titleBrush = new LinearGradientBrush(gradientRect, Color.FromArgb(0, 88, 238), Color.FromArgb(0, 52, 165), LinearGradientMode.Vertical))
            {
                ColorBlend blend = new ColorBlend();
                blend.Positions = new float[] { 0.0f, 0.3f, 0.5f, 0.7f, 1.0f };
                blend.Colors = new Color[] {
                    Color.FromArgb(0, 88, 238),
                    Color.FromArgb(53, 147, 255),
                    Color.FromArgb(40, 142, 255),
                    Color.FromArgb(18, 125, 255),
                    Color.FromArgb(0, 52, 165)
                };
                titleBrush.InterpolationColors = blend;
                g.FillRectangle(titleBrush, gradientRect);
            }

            using (Pen topHighlight = new Pen(Color.FromArgb(120, 180, 220, 255)))
                g.DrawLine(topHighlight, 0, 0, panel.Width, 0);

            using (Pen topHighlight2 = new Pen(Color.FromArgb(80, 160, 210, 255)))
                g.DrawLine(topHighlight2, 0, 1, panel.Width, 1);

            using (Pen bottomShadow = new Pen(Color.FromArgb(100, 0, 30, 80)))
                g.DrawLine(bottomShadow, 0, panel.Height - 1, panel.Width, panel.Height - 1);

            DrawRoundedCorners(g, panel, Color.FromArgb(0, 78, 152));
        }

        private static void DrawRoundedCorners(Graphics g, Panel panel, Color bgColor)
        {
            using (SolidBrush cornerBrush = new SolidBrush(bgColor))
            {
                g.FillRectangle(cornerBrush, 0, 0, 3, 1);
                g.FillRectangle(cornerBrush, 0, 1, 2, 1);
                g.FillRectangle(cornerBrush, 0, 2, 1, 1);
                g.FillRectangle(cornerBrush, panel.Width - 3, 0, 3, 1);
                g.FillRectangle(cornerBrush, panel.Width - 2, 1, 2, 1);
                g.FillRectangle(cornerBrush, panel.Width - 1, 2, 1, 1);
            }
        }

        public static void DrawClientArea(Panel panel, Graphics g)
        {
            using (Pen shadowPen = new Pen(Color.FromArgb(0, 40, 110)))
            {
                g.DrawLine(shadowPen, 0, 0, panel.Width - 1, 0);
                g.DrawLine(shadowPen, 0, 0, 0, panel.Height - 1);
            }

            using (Pen lightPen = new Pen(Color.FromArgb(60, 130, 220)))
            {
                g.DrawLine(lightPen, panel.Width - 1, 0, panel.Width - 1, panel.Height - 1);
                g.DrawLine(lightPen, 0, panel.Height - 1, panel.Width - 1, panel.Height - 1);
            }
        }

        public static void DrawContent(Panel panel, Graphics g)
        {
            using (Pen shadow1 = new Pen(Color.FromArgb(172, 168, 153)))
            {
                g.DrawLine(shadow1, 0, 0, panel.Width - 2, 0);
                g.DrawLine(shadow1, 0, 0, 0, panel.Height - 2);
            }

            using (Pen shadow2 = new Pen(Color.FromArgb(113, 111, 100)))
            {
                g.DrawLine(shadow2, 1, 1, panel.Width - 3, 1);
                g.DrawLine(shadow2, 1, 1, 1, panel.Height - 3);
            }

            using (Pen light1 = new Pen(Color.FromArgb(255, 255, 255)))
            {
                g.DrawLine(light1, panel.Width - 1, 0, panel.Width - 1, panel.Height - 1);
                g.DrawLine(light1, 0, panel.Height - 1, panel.Width - 1, panel.Height - 1);
            }

            using (Pen light2 = new Pen(Color.FromArgb(241, 239, 226)))
            {
                g.DrawLine(light2, panel.Width - 2, 1, panel.Width - 2, panel.Height - 2);
                g.DrawLine(light2, 1, panel.Height - 2, panel.Width - 2, panel.Height - 2);
            }
        }

        public static void DrawButtonXP(Button btn, Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, btn.Width - 1, btn.Height - 1);

            using (LinearGradientBrush btnBrush = new LinearGradientBrush(rect, Color.FromArgb(85, 150, 255), Color.FromArgb(0, 80, 200), LinearGradientMode.Vertical))
            {
                ColorBlend blend = new ColorBlend();
                blend.Positions = new float[] { 0.0f, 0.45f, 0.55f, 1.0f };
                blend.Colors = new Color[] {
                    Color.FromArgb(120, 180, 255),
                    Color.FromArgb(70, 140, 255),
                    Color.FromArgb(50, 120, 240),
                    Color.FromArgb(0, 70, 200)
                };
                btnBrush.InterpolationColors = blend;
                g.FillRectangle(btnBrush, rect);
            }

            using (Pen borderPen = new Pen(Color.FromArgb(0, 50, 140)))
                g.DrawRectangle(borderPen, rect);

            using (Pen highlight = new Pen(Color.FromArgb(150, 200, 220, 255)))
            {
                g.DrawLine(highlight, 1, 1, btn.Width - 2, 1);
                g.DrawLine(highlight, 1, 1, 1, btn.Height / 2);
            }

            using (Pen shadow = new Pen(Color.FromArgb(100, 0, 40, 100)))
            {
                g.DrawLine(shadow, btn.Width - 2, 1, btn.Width - 2, btn.Height - 2);
                g.DrawLine(shadow, 1, btn.Height - 2, btn.Width - 2, btn.Height - 2);
            }

            using (Font marlettFont = new Font("Marlett", 9F))
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                using (Brush shadowBrush = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
                {
                    RectangleF shadowRect = new RectangleF(1, 1, btn.Width, btn.Height);
                    g.DrawString(btn.Text, marlettFont, shadowBrush, shadowRect, sf);
                }

                g.DrawString(btn.Text, marlettFont, Brushes.White, rect, sf);
            }
        }

        public static void DrawButtonCloseXP(Button btn, Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, btn.Width - 1, btn.Height - 1);

            using (LinearGradientBrush btnBrush = new LinearGradientBrush(rect, Color.FromArgb(255, 110, 100), Color.FromArgb(180, 50, 40), LinearGradientMode.Vertical))
            {
                ColorBlend blend = new ColorBlend();
                blend.Positions = new float[] { 0.0f, 0.45f, 0.55f, 1.0f };
                blend.Colors = new Color[] {
                    Color.FromArgb(255, 140, 130),
                    Color.FromArgb(230, 90, 80),
                    Color.FromArgb(210, 70, 60),
                    Color.FromArgb(160, 40, 30)
                };
                btnBrush.InterpolationColors = blend;
                g.FillRectangle(btnBrush, rect);
            }

            using (Pen borderPen = new Pen(Color.FromArgb(120, 30, 20)))
                g.DrawRectangle(borderPen, rect);

            using (Pen highlight = new Pen(Color.FromArgb(150, 255, 180, 170)))
            {
                g.DrawLine(highlight, 1, 1, btn.Width - 2, 1);
                g.DrawLine(highlight, 1, 1, 1, btn.Height / 2);
            }

            using (Pen shadow = new Pen(Color.FromArgb(100, 80, 20, 10)))
            {
                g.DrawLine(shadow, btn.Width - 2, 1, btn.Width - 2, btn.Height - 2);
                g.DrawLine(shadow, 1, btn.Height - 2, btn.Width - 2, btn.Height - 2);
            }

            using (Font marlettFont = new Font("Marlett", 9F))
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                using (Brush shadowBrush = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
                {
                    RectangleF shadowRect = new RectangleF(1, 1, btn.Width, btn.Height);
                    g.DrawString(btn.Text, marlettFont, shadowBrush, shadowRect, sf);
                }

                g.DrawString(btn.Text, marlettFont, Brushes.White, rect, sf);
            }
        }

        public static void DrawXPGroupBox(GroupBox grp, Graphics g)
        {
            SizeF textSize = g.MeasureString(grp.Text, grp.Font);
            int textWidth = (int)textSize.Width;
            int textHeight = (int)textSize.Height;
            int headerY = textHeight / 2;

            Rectangle frameRect = new Rectangle(0, headerY, grp.Width - 1, grp.Height - headerY - 1);

            using (Pen shadowPen = new Pen(Color.FromArgb(130, 140, 160)))
            {
                g.DrawLine(shadowPen, 8 + textWidth + 5, headerY, frameRect.Right, headerY);
                g.DrawLine(shadowPen, 0, headerY, 5, headerY);
                g.DrawLine(shadowPen, 0, headerY, 0, frameRect.Bottom);
            }

            using (Pen lightPen = new Pen(Color.FromArgb(255, 255, 255)))
            {
                g.DrawLine(lightPen, frameRect.Right, headerY, frameRect.Right, frameRect.Bottom);
                g.DrawLine(lightPen, 0, frameRect.Bottom, frameRect.Right, frameRect.Bottom);
            }

            using (Pen innerShadowPen = new Pen(Color.FromArgb(100, 255, 255, 255)))
            {
                g.DrawLine(innerShadowPen, 8 + textWidth + 5, headerY + 1, frameRect.Right - 1, headerY + 1);
                g.DrawLine(innerShadowPen, 6, headerY + 1, 1, headerY + 1);
                g.DrawLine(innerShadowPen, 1, headerY + 1, 1, frameRect.Bottom - 1);
            }

            using (Pen innerLightPen = new Pen(Color.FromArgb(80, 130, 140, 160)))
            {
                g.DrawLine(innerLightPen, frameRect.Right - 1, headerY + 1, frameRect.Right - 1, frameRect.Bottom - 1);
                g.DrawLine(innerLightPen, 1, frameRect.Bottom - 1, frameRect.Right - 1, frameRect.Bottom - 1);
            }
        }
    }
}
