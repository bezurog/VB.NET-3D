Imports System.Drawing
Imports OpenTK
Imports OpenTK.Graphics.OpenGL

Namespace ThreeDlib.Figures

    Public Class Bezier
        Implements IFigure
        Sub New()

        End Sub
        Sub Draw() Implements IFigure.Draw

        End Sub


        Private Sub Draw(p1 As Vector4, p2 As Vector4, p3 As Vector4, p4 As Vector4)
            Dim t As Double = 0
            Dim h As Double = 0.01
            Dim x, y, z As Double
            GL.Begin(BeginMode.Points)
            GL.Color4(Color.Black)
            While t <= 1
                x = Math.Pow(1 - t, 3) * p1.X + 3 * Math.Pow(1 - t, 2) * t * p2.X + 3 * (1 - t) * t * t * p3.X + t * t * t * p4.X
                y = Math.Pow(1 - t, 3) * p1.Y + 3 * Math.Pow(1 - t, 2) * t * p2.Y + 3 * (1 - t) * t * t * p3.Y + t * t * t * p4.Y
                z = Math.Pow(1 - t, 3) * p1.Z + 3 * Math.Pow(1 - t, 2) * t * p2.Z + 3 * (1 - t) * t * t * p3.Z + t * t * t * p4.Z
                GL.Vertex4(New Vector4(x, y, z, 1))
                t += h
            End While
            GL.End()
        End Sub


    End Class



End Namespace
