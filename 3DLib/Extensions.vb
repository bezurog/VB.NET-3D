Imports OpenTK
Imports OpenTK.Graphics
Imports OpenTK.Graphics.OpenGL
Imports System.Drawing 
Imports System.Drawing.Imaging
Imports System.Runtime.CompilerServices
Imports System.IO
Imports OpenGl

Public Module Extensions

    <Extension()>
    Public Sub GrabScreenShot(ByVal glControl As GLControl, fileName As String, Optional pixelType As PixelType = PixelType.UnsignedByte)

            Dim bmp As Bitmap = New Bitmap(glControl.ClientRectangle.Width, glControl.ClientRectangle.Height)
            Dim data As BitmapData = bmp.LockBits(glControl.ClientRectangle, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            GL.ReadPixels(0, 0, glControl.Width, glControl.Height, OpenGL.PixelFormat.Bgr, OpenGL.PixelType.UnsignedByte, data.Scan0)
            bmp.UnlockBits(data)
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY)
            bmp.Save(fileName, ImageFormat.Png)

    End Sub

    <Extension()>
    Public Sub GrabScreenShots(ByVal glControl As GLControl, fileName As String)

        Dim sw = New StreamWriter("screen.log")
        
        Dim pixelTypeEnumConstants As Array = System.Enum.GetValues(GetType(OpenGL.PixelType))
        Dim pixelTypeConstant As Object 
        For Each pixelTypeConstant In pixelTypeEnumConstants
            Dim pixelType As PixelType = DirectCast(pixelTypeConstant, OpenGL.PixelType)
            Dim fileNameByPixel As String = pixelType.ToString() + fileName

            Try
                GrabScreenShot(glControl, fileNameByPixel, pixelType)
                sw.WriteLine("screen for " + pixelType.ToString() + " created")
            Catch ex As Exception
                sw.WriteLine("screen for " + pixelType.ToString() + " errored")
            End Try
        Next

        sw.Close()
    End Sub 


End Module
