﻿Imports OpenTK
Imports OpenTK.Graphics
Imports OpenTK.Graphics.OpenGL
Imports System.Drawing
Imports System.Drawing.Imaging
Imports ThreeDlib
Imports ThreeDlib.Figures

Public Class Form1
    Enum Orto
        OX
        OY
        OZ
    End Enum

    Dim isPress As Boolean = False
    Dim wheel As Integer = 0
    Const MAXWHEEL = 5
    
    Dim camera As Vector3
    Dim radiusCamera As Double = 340

    Dim fi As Double = Math.PI / 4
    Dim dFi As Double = 0

    Dim teta As Double = Math.PI / 3
    Dim dTeta As Double = 0

    Dim X As Integer = 0
    Dim dxy As Integer = 0
    Dim Y As Integer = 0
    Dim dz As Integer = 0

    Dim figures As List(Of Figure)

    Private Sub GlControl1_Load(sender As Object, e As EventArgs) Handles GlControl1.Load
        GL.ClearColor(Color.DarkGray)
        camera = New Vector3(0,0,0)
        CreateFigures()
        UpdateLabel()
    End Sub

    Private Sub CreateFigures()
        figures = New List(Of Figure)
        figures.Add(FigureCreator.CreateLine(New Vector4(-15, -15, 150, 1), New Vector4(-15, -15, 10, 1), 2, False))
        figures.Add(FigureCreator.CreateLine(New Vector4(15, -15, 150, 1), New Vector4(15, -15, 10, 1), 2, False))
        figures.Add(FigureCreator.CreateLine(New Vector4(15, 15, 10, 1), New Vector4(15, 15, 150, 1), 2, False))
        figures.Add(FigureCreator.CreateLine(New Vector4(-15, 15, 10, 1), New Vector4(-15, 15, 150, 1), 2, False))
        figures.Add(FigureCreator.CreateLine(New Vector4(0, 0, 10, 1), New Vector4(0, 0, 155, 1), 5, False))
        figures.Add(FigureCreator.CreatePipe(New Vector4(-40, 0, 80, 1), New Vector4(0, 0, 80, 1), Color.Green, 20, 8, True))
        Dim pipe1 As Pipe = FigureCreator.CreatePipe(New Vector4(0, 0, 0, 1), New Vector4(0, 0, 155, 1), New Color4(0.2f, 0.8f, 0.5f, 0.3f), 5, 8, False)
        Dim pipe2 As Pipe = FigureCreator.CreatePipe(New Vector4(0, 0, 155, 1), New Vector4(0, -120, 155, 1), Color.Magenta, 5, 8, False)
        Dim pipe3 As Pipe = FigureCreator.CreatePipe(New Vector4(80, -120, 155, 1), New Vector4(0, -120, 155, 1), New Color4(1f, 0.5f, 0.5f, 0.5f), 5, 8, False)
        Dim pipe4 As Pipe = FigureCreator.CreatePipe(New Vector4(0, 0, 0, 1), New Vector4(0, 100, 0, 1), Color.Gray, 5, 8, False)

        figures.Add(pipe1)
        figures.Add(pipe2)
        figures.Add(pipe3)
        figures.Add(pipe4)
        figures.Add(FigureCreator.CreatePipeConnector(pipe1, pipe2, 30))
        figures.Add(FigureCreator.CreatePipeConnector(pipe2, pipe3, 10))
        figures.Add(FigureCreator.CreatePipeConnector(pipe1, pipe4, 5))
        figures.Add(FigureCreator.CreateLine(New Vector4(-40, -50, 10, 1), New Vector4(-40, 50, 10, 1), 1, True))
        figures.Add(FigureCreator.CreateLine(New Vector4(-50, -40, 10, 1), New Vector4(50, -40, 10, 1), 1, True))

        figures.Add(FigureCreator.CreateRect(New Vector4(-15, -15, 150, 1), New Vector4(15, -15, 150, 1), New Vector4(15, 15, 150, 1), 
                                                     New Vector4(-15, 15, 150, 1), Color.Blue, True, False))
        figures.Add(FigureCreator.CreateRect(New Vector4(-15, -15, 90, 1), New Vector4(15, -15, 90, 1), New Vector4(15, 15, 90, 1),
                                                     New Vector4(-15, 15, 90, 1), New Color4(1.0F, 1.0F, 1.0F, 1.0F), True, True))
        figures.Add(FigureCreator.CreateRect(New Vector4(-15, -15, 90, 1), New Vector4(15, -15, 90, 1), New Vector4(15, 15, 90, 1),
                                                     New Vector4(-15, 15, 90, 1), New Color4(1.0F, 1.0F, 1.0F, 1.0F), True, True))
        figures.Add(FigureCreator.CreateRect(New Vector4(-15, -15, 10, 1), New Vector4(15, -15, 10, 1), New Vector4(15, 15, 10, 1),
                                                    New Vector4(-15, 15, 10, 1), Color.Blue, True, True))
        figures.Add(FigureCreator.CreateParallel(New Vector4(-25, -25, 40, 1),
                 New Vector4(25, -25, 40, 1),
                 New Vector4(-25, 25, 40, 1),
                 New Vector4(25, 25, 40, 1),
                 New Vector4(-25, 25, 150, 1),
                 New Vector4(25, 25, 150, 1),
                 New Vector4(-25, -25, 150, 1),
                 New Vector4(25, -25, 150, 1),
                 Color.HotPink, True, False))
        figures.Add(FigureCreator.CreateParallel(New Vector4(-40, -40, 10, 1),
                 New Vector4(40, -40, 10, 1),
                 New Vector4(-40, 40, 10, 1),
                 New Vector4(40, 40, 10, 1),
                 New Vector4(-40, 40, 40, 1),
                 New Vector4(40, 40, 40, 1),
                 New Vector4(-40, -40, 40, 1),
                 New Vector4(40, -40, 40, 1),
                 Color.DarkGreen, True, True))
        figures.Add(FigureCreator.CreateParallel(New Vector4(-50, -50, 0, 1),
                 New Vector4(50, -50, 0, 1),
                 New Vector4(-50, 50, 0, 1),
                 New Vector4(50, 50, 0, 1),
                 New Vector4(-50, 50, 10, 1),
                 New Vector4(50, 50, 10, 1),
                 New Vector4(-50, -50, 10, 1),
                 New Vector4(50, -50, 10, 1),
                 Color.IndianRed, True, False))
    End Sub

    Private Sub GlControl1_Paint(sender As Object, e As PaintEventArgs) Handles GlControl1.Paint
        'Clear buffers

        GL.Clear(ClearBufferMask.ColorBufferBit)
        GL.Clear(ClearBufferMask.DepthBufferBit)

        'View settings
        Dim perspective As Matrix4 = Matrix4.CreatePerspectiveFieldOfView(Math.PI / 3, 1, 1, 1000) 'perspective

        camera.X = radiusCamera * Math.Sin(teta + dTeta) * Math.Cos(fi + dfi)
        camera.Y = radiusCamera * Math.Sin(teta + dTeta) * Math.Sin(fi + dfi)
        camera.Z = radiusCamera * Math.Cos(teta + dTeta)

        Dim lookAt As Matrix4 = Matrix4.LookAt(camera.X, camera.Y, camera.Z, 0, 0, 0, 0, 0, 1)    'camera
        GL.MatrixMode(MatrixMode.Projection)
        'GL.LoadIdentity()
        GL.LoadMatrix(perspective)          'load perspective

        GL.MatrixMode(MatrixMode.Modelview)
        'GL.LoadIdentity()
        GL.LoadMatrix(lookAt)               'load camera

        GL.Viewport(0, 0, GlControl1.Width, GlControl1.Height)  'window size
        GL.Enable(EnableCap.DepthTest)
        GL.DepthFunc(DepthFunction.Less)

        'Scale
        GL.Scale(1 + wheel * 0.1, 1 + wheel * 0.1, 1 + wheel * 0.1)

        DrawFigure()
         
        GlControl1.SwapBuffers()  'Takes from the GL and puts into control

    End Sub

    Private Sub DrawFigure()
        For Each f In figures
            if f IsNot Nothing Then f.Draw()
        Next
            
        'DrawTexture("bitmap.bmp", New Vector4(-50, -50, 0, 1),
        '         New Vector4(50, -50, 0, 1),
        '         New Vector4(-50, 50, 0, 1),
        '         New Vector4(50, 50, 0, 1))

    End Sub

    'Public Sub LoadTexture(bmp As Bitmap)
    '    Dim Data As BitmapData = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height),
    '        ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb)
    '    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, Data.Width, Data.Height, 0,
    '            OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, Data.Scan0)
    '    bmp.UnlockBits(Data)
    'End Sub

    'Private Function GetBitmap(fileName As String) As Bitmap
    '    Dim bmpTex As Bitmap
    '    bmpTex = New Bitmap(fileName)
    '    Return bmpTex
    'End Function

    'Private Sub DrawTexture(fileName As String, p1 As Vector4, p2 As Vector4, p3 As Vector4, p4 As Vector4)
    '    LoadTexture(GetBitmap(fileName))
    '    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureMinFilter.Nearest)
    '    GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear)
    '    GL.Enable(EnableCap.Texture2D)

    '    GL.Begin(BeginMode.Quads)
    '    GL.TexCoord3(0, 0, 0)
    '    GL.Vertex4(p1)
    '    GL.TexCoord3(0, 1, 1)
    '    GL.Vertex4(p2)
    '    GL.TexCoord3(1, 1, 0)
    '    GL.Vertex4(p3)
    '    GL.TexCoord3(1, 0, 0)
    '    GL.Vertex4(p4)
    '    GL.End()

    '    GL.Disable(EnableCap.Texture2D)

    'End Sub

    Private Sub GlControl1_MouseDown(sender As Object, e As MouseEventArgs) Handles GlControl1.MouseDown
        If e.Button = MouseButtons.Left Then
            isPress = True
            X = e.X
            Y = e.Y
            dFi = 0
            dTeta = 0
        End If
        UpdateLabel()
    End Sub

    Private Sub GlControl1_MouseMove(sender As Object, e As MouseEventArgs) Handles GlControl1.MouseMove
        If isPress Then
            
            dxy = e.X - X
            dz = e.Y - Y

            dFi = dxy * 2 * Math.PI / GlControl1.Width
            dTeta = dz * Math.PI / GlControl1.Height

            GlControl1.Invalidate()
            UpdateLabel()
        End If
    End Sub

    Private Sub GlControl1_MouseUp(sender As Object, e As MouseEventArgs) Handles GlControl1.MouseUp
        isPress = False
        dxy = 0
        dz = 0
        fi += dfi
        teta += dTeta
        UpdateLabel()
    End Sub

    Private Sub GlControl1_MouseWheel(sender As Object, e As MouseEventArgs) Handles GlControl1.MouseWheel
        wheel += e.Delta / 120
        If wheel > MAXWHEEL Then wheel = MAXWHEEL
        If wheel < -MAXWHEEL Then wheel = -MAXWHEEL
        GlControl1.Invalidate()
    End Sub


    Private Sub UpdateLabel()
        TextBox1.Text = dxy.ToString()
        TextBox2.Text = dz.ToString()
        tbCameraX.Text = camera.X.ToString()
        tbCameraY.Text = camera.Y.ToString()
        tbCameraZ.Text = camera.Z.ToString()



    End Sub

End Class
