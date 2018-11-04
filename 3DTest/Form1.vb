Imports OpenTK
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
    Dim X As Integer = 0
    Dim Y As Integer = 0

    Dim dxy As Integer = 0
    Dim dz As Integer = 0

    Private Sub GlControl1_Load(sender As Object, e As EventArgs) Handles GlControl1.Load
        GL.ClearColor(Color.DarkGray)
    End Sub

    Private Sub GlControl1_Paint(sender As Object, e As PaintEventArgs) Handles GlControl1.Paint
        'Clear buffers

        GL.Clear(ClearBufferMask.ColorBufferBit)
        GL.Clear(ClearBufferMask.DepthBufferBit)

        'View settings
        Dim perspective As Matrix4 = Matrix4.CreatePerspectiveFieldOfView(Math.PI / 3, 1, 1, 1000) 'perspective
        Dim lookAt As Matrix4 = Matrix4.LookAt(150, 150, 300, 0, 0, 0, 0, 0, 1)    'camera
        GL.MatrixMode(MatrixMode.Projection)
        'GL.LoadIdentity()
        GL.LoadMatrix(perspective)          'load perspective

        GL.MatrixMode(MatrixMode.Modelview)
        'GL.LoadIdentity()
        GL.LoadMatrix(lookAt)               'load camera

        GL.Viewport(0, 0, GlControl1.Width, GlControl1.Height)  'window size
        GL.Enable(EnableCap.DepthTest)
        GL.DepthFunc(DepthFunction.Less)

        'Rotate
        GL.Rotate(dxy / (GlControl1.Width * 0.005), 0, 0, 1)
        GL.Rotate(dz / (GlControl1.Height * 0.005), 0, 1, 0)

        'Scale
        GL.Scale(1 + wheel * 0.1, 1 + wheel * 0.1, 1 + wheel * 0.1)

        DrawFigure()

        GlControl1.SwapBuffers()  'Takes from the GL and puts into control

    End Sub

    Private Sub DrawFigure()

        DrawLine(New Vector4(-15, -15, 150, 1), New Vector4(-15, -15, 10, 1), 2, False)
        DrawLine(New Vector4(15, -15, 150, 1), New Vector4(15, -15, 10, 1), 2, False)
        DrawLine(New Vector4(15, 15, 10, 1), New Vector4(15, 15, 150, 1), 2, False)
        DrawLine(New Vector4(-15, 15, 10, 1), New Vector4(-15, 15, 150, 1), 2, False)
        DrawLine(New Vector4(0, 0, 10, 1), New Vector4(0, 0, 155, 1), 5, False)

        Dim pipe1 As Pipe = FigureCreator.CreatePipe(New Vector4(0, 0, 0, 1), New Vector4(0, 0, 155, 1), Color.Red, 20, 8, True)
        Dim pipe2 As Pipe = FigureCreator.CreatePipe(New Vector4(0, 0, 155, 1), New Vector4(0, 90, 155, 1), Color.Blue, 20, 8, True)
        'Pipe.Connect(pipe1, pipe2, 15)

        Dim pipe3 As Pipe = FigureCreator.CreatePipe(New Vector4(-40, 0, 80, 1), New Vector4(0, 0, 80, 1), Color.Green, 20, 8, True)
        'Pipe.Connect(pipe1, pipe3, 20)
        Dim pipe4 As Pipe = FigureCreator.CreatePipe(New Vector4(0, 0, 155, 1), New Vector4(0, -120, 155, 1), Color.Magenta, 20, 8, True)



        Pipe.Connect(pipe1, pipe4, 100)
        pipe3.Draw()




        'Pipe.Connect(pipe1, pipe3, 3)

        DrawLine(New Vector4(-40, -50, 10, 1), New Vector4(-40, 50, 10, 1), 1, True)
        DrawLine(New Vector4(-50, -40, 10, 1), New Vector4(50, -40, 10, 1), 1, True)

        'For i As Integer = 0 To 5
        '    DrawLine(New Vector4(-40 + i * 15 + 3, -40, 10, 1), New Vector4(-40 + i * 15 + 3, 40, 10, 1), 2, False)
        '    DrawLine(New Vector4(-40, -40 + i * 15 + 3, 10, 1), New Vector4(40, -40 + i * 15 + 3, 10, 1), 2, False)
        'Next

        'For i As Integer = 0 To 5
        '    DrawLine(New Vector4(-40 + i * 15 + 3, -40, 40, 1), New Vector4(-40 + i * 15 + 3, 40, 40, 1), 2, False)
        '    DrawLine(New Vector4(-40, -40 + i * 15 + 3, 40, 1), New Vector4(40, -40 + i * 15 + 3, 40, 1), 2, False)
        'Next

        'DrawBezier(New Vector4(0, 0, 155, 1), New Vector4(-5, -5, 165, 1), New Vector4(-10, -10, 150, 1), New Vector4(-15, -15, 150, 1))
        'DrawBezier(New Vector4(0, 0, 155, 1), New Vector4(5, -5, 165, 1), New Vector4(10, -10, 150, 1), New Vector4(15, -15, 150, 1))
        'DrawBezier(New Vector4(0, 0, 155, 1), New Vector4(5, 5, 165, 1), New Vector4(10, 10, 150, 1), New Vector4(15, 15, 150, 1))
        'DrawBezier(New Vector4(0, 0, 155, 1), New Vector4(-5, 5, 165, 1), New Vector4(-10, 10, 150, 1), New Vector4(-15, 15, 150, 1))

        DrawRect(New Vector4(-15, -15, 150, 1),
                 New Vector4(15, -15, 150, 1),
                 New Vector4(15, 15, 150, 1),
                 New Vector4(-15, 15, 150, 1),
                 New Color4(1.0F, 1.0F, 1.0F, 1.0F), True, False)

        DrawRect(New Vector4(-15, -15, 90, 1),
                New Vector4(15, -15, 90, 1),
                New Vector4(15, 15, 90, 1),
                New Vector4(-15, 15, 90, 1),
                New Color4(1.0F, 1.0F, 1.0F, 1.0F), True, False)

        DrawRect(New Vector4(-15, -15, 10, 1),
                New Vector4(15, -15, 10, 1),
                New Vector4(15, 15, 10, 1),
                New Vector4(-15, 15, 10, 1),
                New Color4(1.0F, 1.0F, 1.0F, 1.0F), True, False)

        DrawParallel(New Vector4(-25, -25, 40, 1),
                 New Vector4(25, -25, 40, 1),
                 New Vector4(-25, 25, 40, 1),
                 New Vector4(25, 25, 40, 1),
                 New Vector4(-25, 25, 150, 1),
                 New Vector4(25, 25, 150, 1),
                 New Vector4(-25, -25, 150, 1),
                 New Vector4(25, -25, 150, 1),
                 New Color4(0.83F, 0.83F, 0.83F, 0.0F), True, False)

        DrawParallel(New Vector4(-40, -40, 10, 1),
                 New Vector4(40, -40, 10, 1),
                 New Vector4(-40, 40, 10, 1),
                 New Vector4(40, 40, 10, 1),
                 New Vector4(-40, 40, 40, 1),
                 New Vector4(40, 40, 40, 1),
                 New Vector4(-40, -40, 40, 1),
                 New Vector4(40, -40, 40, 1),
                 New Color4(0.83F, 0.83F, 0.83F, 0.0F), True, True)

        DrawParallel(New Vector4(-50, -50, 0, 1),
                 New Vector4(50, -50, 0, 1),
                 New Vector4(-50, 50, 0, 1),
                 New Vector4(50, 50, 0, 1),
                 New Vector4(-50, 50, 10, 1),
                 New Vector4(50, 50, 10, 1),
                 New Vector4(-50, -50, 10, 1),
                 New Vector4(50, -50, 10, 1),
                 New Color4(0.83F, 0.83F, 0.83F, 1.0F), True, True)

        'DrawTexture("bitmap.bmp", New Vector4(-50, -50, 0, 1),
        '         New Vector4(50, -50, 0, 1),
        '         New Vector4(-50, 50, 0, 1),
        '         New Vector4(50, 50, 0, 1))

    End Sub

    Private Sub DrawLine(p1 As Vector4, p2 As Vector4, lineWidth As Integer, isStipple As Boolean)
        GL.LineWidth(lineWidth)
        If isStipple Then
            GL.Enable(EnableCap.LineSmooth)
            GL.Enable(EnableCap.LineStipple)
            GL.LineStipple(2, 255)
        End If
        GL.Begin(BeginMode.Lines)
        GL.Color4(Color.Black)
        GL.Vertex4(p1)
        GL.Vertex4(p2)
        GL.End()
        If isStipple Then
            GL.Disable(EnableCap.LineSmooth)
            GL.Disable(EnableCap.LineStipple)
        End If
        GL.LineWidth(1)
    End Sub

    Private Sub DrawRect(p1 As Vector4, p2 As Vector4, p3 As Vector4, p4 As Vector4, clr As Color, isBorders As Boolean, isFaces As Boolean)
        GL.Enable(EnableCap.Blend)
        GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha)

        If isBorders Then
            GL.Begin(BeginMode.LineLoop)
            GL.Color4(Color.Black)
            GL.Vertex4(p1)
            GL.Vertex4(p2)
            GL.Vertex4(p3)
            GL.Vertex4(p4)
            GL.End()
        End If

        If isFaces Then
            GL.Begin(BeginMode.Quads)
            GL.Color4(clr)
            GL.Vertex4(p1)
            GL.Vertex4(p2)
            GL.Vertex4(p3)
            GL.Vertex4(p4)
            GL.End()
        End If

        GL.Disable(EnableCap.Blend)
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


    Private Sub DrawParallel(p1 As Vector4, p2 As Vector4, p3 As Vector4, p4 As Vector4, p5 As Vector4, p6 As Vector4,
                             p7 As Vector4, p8 As Vector4, clr1 As Color, isBorders As Boolean, isFaces As Boolean)
        GL.Enable(EnableCap.Blend)
        GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha)

        If isBorders Then
            GL.Enable(EnableCap.LineSmooth)

            GL.Begin(BeginMode.LineStrip)
            GL.Color4(Color.Black)
            GL.Vertex4(p1)
            GL.Vertex4(p2)
            GL.Vertex4(p4)
            GL.Vertex4(p3)
            GL.Vertex4(p1)
            GL.Vertex4(p7)
            GL.Vertex4(p8)
            GL.Vertex4(p6)
            GL.Vertex4(p5)
            GL.Vertex4(p7)
            GL.End()

            GL.Begin(BeginMode.LineLoop)
            GL.Vertex4(p5)
            GL.Vertex4(p6)
            GL.Vertex4(p8)
            GL.Vertex4(p7)
            GL.End()

            GL.Begin(BeginMode.LineLoop)
            GL.Vertex4(p1)
            GL.Vertex4(p2)
            GL.Vertex4(p4)
            GL.Vertex4(p3)
            GL.End()

            GL.Begin(BeginMode.Lines)
            GL.Vertex4(p1)
            GL.Vertex4(p7)
            GL.Vertex4(p2)
            GL.Vertex4(p8)
            GL.Vertex4(p3)
            GL.Vertex4(p5)
            GL.Vertex4(p4)
            GL.Vertex4(p6)
            GL.End()
        End If

        If isFaces Then


            GL.Vertex4(p1)
            GL.Vertex4(p2)
            GL.Vertex4(p3)
            GL.Vertex4(p4)
            GL.Vertex4(p5)
            GL.Vertex4(p6)
            GL.Vertex4(p7)
            GL.Vertex4(p8)
            GL.Vertex4(p1)
            GL.Vertex4(p2)
            GL.End()

            GL.Begin(BeginMode.Quads)
            'GL.Color3(clr2)
            GL.Vertex4(p2)
            GL.Vertex4(p4)
            GL.Vertex4(p6)
            GL.Vertex4(p8)
            GL.Vertex4(p1)
            GL.Vertex4(p3)
            GL.Vertex4(p5)
            GL.Vertex4(p7)
            GL.End()

        End If

        GL.Disable(EnableCap.Blend)
    End Sub

    Private Sub GlControl1_MouseDown(sender As Object, e As MouseEventArgs) Handles GlControl1.MouseDown
        If e.Button = MouseButtons.Left Then
            isPress = True
            X = e.X
            Y = e.Y
        End If
    End Sub

    Private Sub GlControl1_MouseMove(sender As Object, e As MouseEventArgs) Handles GlControl1.MouseMove
        If isPress Then
            dxy = e.X - X
            dz = e.Y - Y
            GlControl1.Invalidate()
            'UpdateLabel()
        End If
    End Sub

    Private Sub GlControl1_MouseUp(sender As Object, e As MouseEventArgs) Handles GlControl1.MouseUp
        isPress = False
    End Sub

    Private Sub GlControl1_MouseWheel(sender As Object, e As MouseEventArgs) Handles GlControl1.MouseWheel
        wheel += e.Delta / 120
        If wheel > MAXWHEEL Then wheel = MAXWHEEL
        If wheel < -MAXWHEEL Then wheel = -MAXWHEEL
        GlControl1.Invalidate()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        dxy = 0
        dz = 0
        GlControl1.Invalidate()
        'UpdateLabel()
    End Sub

End Class
