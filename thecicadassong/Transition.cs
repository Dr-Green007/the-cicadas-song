using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace thecicadassong
{
    public static class Transition
    {
        public static void Open(string sceneId, GameState state, Form? current = null)
        {
            Form next;
            switch (sceneId)
            {
                case "splash":
                    next = new FormPrologue(state);   //заставка
                    break;
                case "prologue":
                    next = new FormPrologue01(state); //сцена с кнопками
                    break;
                default:
                    MessageBox.Show($"Сцена «{sceneId}» не реализована.", "Песнь цикад",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            next.StartPosition = FormStartPosition.Manual;
            next.Location = current?.Location ?? new Point(200, 100);
            next.Show();

            if (current is Form1)
                current.Hide();
            else
                current?.Close();
        }
    }
}