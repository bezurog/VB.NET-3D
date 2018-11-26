Imports OpenTK.Graphics
Imports System.Collections.Generic

Namespace ThreeDlib.Figures


    Public MustInherit Class Figure
        Protected Dim isValid_ As Boolean = False
        Public Property IsEnable() As Boolean = True
        Public Property Color() As Color4
        Public Property Name() As String
        Public Property Ige() As String
        Private Shared idCounters_ As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)

        Public ReadOnly Property IsValid As Boolean 
            Get
                Return isValid_
            End Get
        End Property

        Public Sub New(color As Color4, Optional Name As String = Nothing)
            Me.Color = color
            If NOT idCounters_.ContainsKey(Type) Then idCounters_.Add(Type, 0)
            idCounters_(Type) += 1
            Me.Name = If(String.IsNullOrEmpty(Name), Me.Type + idCounters_(Type).ToString(), Name)
        End Sub
        
        Private type_ As String = GetFigureType()
        Public ReadOnly Property Type As String
            Get
                'If String.IsNullOrEmpty(type_) Then type_ = GetFigureType()
                Return type_
            End Get
        End Property

        Protected MustOverride Function Init() As Boolean
        Public MustOverride Sub Draw()

        Private Function GetFigureType() As String 
            Dim type As String = Me.GetType().ToString()
            Dim dotInd As String = type.LastIndexOf(".")
            Return type.Substring(dotInd + 1, type.Length - dotInd - 1)
        End Function

        Public Overrides Function ToString() As String
            Return Me.Name
        End Function

    End Class

End Namespace

