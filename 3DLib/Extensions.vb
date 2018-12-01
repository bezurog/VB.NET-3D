Imports OpenTK
Imports OpenTK.Graphics
Imports OpenTK.Graphics.OpenGL
Imports System.Drawing 
Imports System.Drawing.Imaging
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms
Imports OpenGl

Public Module Extensions

    <Extension()>
    Public Function GrabScreenShot(ByVal glControl As GLControl) As BitMap
        'glControl.Invalidate()

        If GraphicsContext.CurrentContext Is Nothing Then
            MsgBox("Oшибка инициализации, графический контекст пуст")
            Return Nothing
        End If

        Dim bmp As Bitmap = New Bitmap(glControl.ClientRectangle.Width, glControl.ClientRectangle.Height)
        Dim data As BitmapData = bmp.LockBits(glControl.ClientRectangle, ImageLockMode.WriteOnly, 
                                                  System.Drawing.Imaging.PixelFormat.Format24bppRgb)
        GL.ReadPixels(0, 0, glControl.ClientRectangle.Width, glControl.ClientRectangle.Height, 
                          OpenGL.PixelFormat.Bgr,OpenGL.PixelType.UnsignedByte, data.Scan0)
        bmp.UnlockBits(data)
        bmp.RotateFlip(RotateFlipType.RotateNoneFlipY)
        
        Return bmp

    End Function

    <Extension()>
    Public Sub GrabScreenShotToClipboard(ByVal glControl As GLControl)
        Dim bitmap = glControl.GrabScreenShot() 
        If bitmap Is Nothing Then Return 

        Try 
            Clipboard.Clear()
            Clipboard.SetImage(bitmap)
        Catch ex As Exception
            MsgBox("Не удалось скопироват скриншот в буфер обмена" & vbCrLf & ex.Message)
        End Try
    End Sub

    <Extension()>
    Public Sub GrabScreenShotToFile(ByVal glControl As GLControl, Optional fileName As String = Nothing)
        Dim bitmap = glControl.GrabScreenShot()
        If bitmap Is Nothing Then Return 

        If fileName Is Nothing Or String.IsNullOrWhiteSpace(fileName) Then fileName = "screen.png"
        Try 
            bitmap.Save(fileName)
        Catch ex As Exception
            MsgBox("Не удалось сохранить скриншот в файл " & fileName & vbCrLf & ex.Message)
        End Try

    End Sub

End Module
