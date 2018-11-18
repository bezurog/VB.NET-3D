Imports OpenTK
Imports OpenTK.Graphics
Imports OpenTK.Graphics.OpenGL
Imports System.Drawing 
Imports System.Drawing.Imaging
Imports System.Runtime.CompilerServices

Public Module Extensions

    <Extension()>
    Public Sub GrabScreenShot(ByVal glControl As GLControl, fileName As String)

            Dim bmp As Bitmap = New Bitmap(glControl.ClientRectangle.Width, glControl.ClientRectangle.Height)
            Dim data As BitmapData = bmp.LockBits(glControl.ClientRectangle, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            GL.ReadPixels(0, 0, glControl.Width, glControl.Height, OpenGL.PixelFormat.Bgr, OpenGL.PixelType.UnsignedByte, data.Scan0)
            bmp.UnlockBits(data)
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY)
            bmp.Save(fileName, ImageFormat.Png)

    End Sub

End Module
