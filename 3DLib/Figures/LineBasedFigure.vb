Imports OpenTK
Imports OpenTK.Graphics
Imports OpenTK.Graphics.OpenGL
Imports System.Drawing

Namespace ThreeDlib.Figures
    Public MustInherit Class LineBasedFigure
        Inherits Figure

        Public Property LineWidth() As Double

        Sub New(clr As Color, lineWidth As Double, Optional Name As String = Nothing)
            MyBase.New(clr, Name)
            Me.LineWidth = lineWidth
        End Sub

    End Class
End Namespace
