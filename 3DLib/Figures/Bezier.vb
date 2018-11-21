Imports System.Drawing
Imports OpenTK
Imports OpenTK.Graphics.OpenGL

Namespace ThreeDlib.Figures

    Public Class Bezier
        Inherits LineBasedFigure

        Dim vertexes_() As Vector4
        Dim vertexesCount_ As Integer

        Friend Property P1() As Vector4
        Friend Property P2() As Vector4
        Friend Property P3() As Vector4
        Friend Property P4() As Vector4
        Public Property Color() As Color

        Sub New(p1 As Vector4, p2 As Vector4, p3 As Vector4, p4 As Vector4, color As Color, 
                Optional lineWidth As Double = 1, Optional Name As String = Nothing)
            MyBase.New(color, lineWidth, Name)
            Me.P1 = p1
            Me.P2 = p2
            Me.P3 = p3
            Me.P4 = p4
            Me.Color = color
            Me.isValid_ = Init()
        End Sub

        Protected Overrides Function Init() As Boolean 
            Dim t As Double = 0
            Dim h As Double = 0.01
            vertexesCount_ = 1 / h + 1
            ReDim vertexes_(vertexesCount_ - 1)

            For i = 0 To vertexesCount_ - 1
                vertexes_(i).X = Math.Pow(1 - t, 3) * p1.X + 3 * Math.Pow(1 - t, 2) * t * p2.X + 3 * (1 - t) * t * t * p3.X + t * t * t * p4.X
                vertexes_(i).Y  = Math.Pow(1 - t, 3) * p1.Y + 3 * Math.Pow(1 - t, 2) * t * p2.Y + 3 * (1 - t) * t * t * p3.Y + t * t * t * p4.Y
                vertexes_(i).Z  = Math.Pow(1 - t, 3) * p1.Z + 3 * Math.Pow(1 - t, 2) * t * p2.Z + 3 * (1 - t) * t * t * p3.Z + t * t * t * p4.Z   
                vertexes_(i).W = 1
                t += h
            Next i
            Return True
        End Function

        Public Overrides Sub Draw()
            If Not IsEnable Then Return

            GL.LineWidth(LineWidth)
            GL.Begin(BeginMode.Points)
            GL.Color4(Me.Color)
            For i = 0 To vertexesCount_ - 1
                GL.Vertex4(New Vector4(vertexes_(i)))
            Next i
            GL.End()
            GL.LineWidth(1)
        End Sub
    End Class
End Namespace
