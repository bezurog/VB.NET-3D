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
            Dim pipe As Pipe = New Pipe(p1, p2, clr, width, vertexesCount, isBorders)
            If Not pipe.IsValid Then pipe = Nothing
            Return pipe
        End Function

        Public Shared Function CreatePipeConnector(pipe1 As Pipe, pipe2 As Pipe, radius As Double) As PipeConnector
            Dim connector As PipeConnector = New PipeConnector(pipe1, pipe2, radius)
            If Not connector.IsValid Then connector = Nothing
            Return connector
        End Function

        Public Shared Function CreateLine(p1 As Vector4, p2 As Vector4, lineWidth As Integer, isStipple As Boolean) As Line
            Dim line As Line = New Line(p1, p2, lineWidth, isStipple)
            If Not line.IsValid Then line = Nothing
            Return line
        End Function

        Public Shared Function CreateRect(p1 As Vector4, p2 As Vector4, p3 As Vector4, p4 As Vector4, clr As Color, isBorders As Boolean, isFaces As Boolean) As Rect
            Dim rect As Rect = New Rect(p1, p2, p3, p4, clr, isBorders, isFaces)
            If Not rect.IsValid Then rect = Nothing
            Return rect
        End Function

        Public Shared Function CreateParallel(p1 As Vector4, p2 As Vector4, p3 As Vector4, p4 As Vector4, 
               p5 As Vector4, p6 As Vector4, p7 As Vector4, p8 As Vector4, clr As Color, isBorders As Boolean, isFaces As Boolean) As Parallel 

            Dim parallel As Parallel = New Parallel(p1, p2, p3, p4, p5, p6, p7, p8, clr, isBorders, isFaces)
            If Not parallel.IsValid Then parallel = Nothing
            Return parallel
        End Function

    End Class
End Namespace