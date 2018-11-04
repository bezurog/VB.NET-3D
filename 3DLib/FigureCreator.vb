Imports System.Drawing
Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Imports ThreeDlib.Figures


Namespace ThreeDlib

    'DrawLine(p1 As Vector4, p2 As Vector4, lineWidth As Integer, isStipple As Boolean)
    'DrawBezier(p1 As Vector4, p2 As Vector4, p3 As Vector4, p4 As Vector4)
    'DrawRect(p1 As Vector4, p2 As Vector4, p3 As Vector4, p4 As Vector4, clr As Color, isBorders As Boolean, isFaces As Boolean)
    'DrawParallel(p1 As Vector4, p2 As Vector4, p3 As Vector4, p4 As Vector4, p5 As Vector4, p6 As Vector4, p7 As Vector4, p8 As Vector4, 
    'clr1 As Color, isBorders As Boolean, isFaces As Boolean)
    'Create(p1 As Vector4, p2 As Vector4, clr As Color, width As Double, vertexesCount As Integer, isBorders As Boolean)
    Public Class FigureCreator
        Public Shared Function CreatePipe(p1 As Vector4, p2 As Vector4, clr As Color, width As Double, vertexesCount As Integer, isBorders As Boolean) As Pipe
            Dim pipe As Pipe = New Pipe(p1, p2)
            If Not pipe.IsValid Then pipe = Nothing Else pipe.Init(clr, width, vertexesCount, isBorders)
            Return pipe
        End Function


    End Class
End Namespace