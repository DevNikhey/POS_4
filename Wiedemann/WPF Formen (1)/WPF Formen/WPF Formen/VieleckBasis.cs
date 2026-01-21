using System;
using System.Windows;

namespace WPF_Formen
{
    internal abstract class VieleckBasis : Basis
    {
        protected Point[] BerechnePunkte(
            int anzahl,
            double radius,
            double startWinkel = -90)
        {
            Point[] punkte = new Point[anzahl];

            double winkelSchritt = 2 * Math.PI / anzahl;
            double winkel = startWinkel * Math.PI / 180;

            for (int i = 0; i < anzahl; i++)
            {
                punkte[i] = new Point(
                    X1 + radius * Math.Cos(winkel),
                    Y1 + radius * Math.Sin(winkel)
                );

                winkel += winkelSchritt;
            }

            return punkte;
        }
    }
}
