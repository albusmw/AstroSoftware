Option Explicit On
Option Strict On
Imports ASCOM.Tools.Novas31

Public Class cDB

    Public novas As New ASCOM.Tools.Novas31.Novas

    Public Properties As New cProperties

    Public Class cProperties

        '''<summary>Geodetic (ITRS) latitude; north positive (degrees)</summary>
        Public Property MyLat As Double = Double.NaN

        '''<summary>Geodetic (ITRS) longitude; east positive (degrees)</summary>
        Public Property MyLong As Double = Double.NaN

        '''<summary>Observer's height above sea level</summary>
        Public Property MyHeight As Double = Double.NaN

        '''<summary>Observer's location's ambient temperature (degrees Celsius)</summary>
        Public Property MyTemperature As Double = 0.0

        '''<summary>Observer's location's atmospheric pressure (millibars)</summary>
        Public Property MyPressure As Double = 1013.0

        Public Sub SetToHolzkirchen()
            MyLat = 47.878345
            MyLong = 11.691738
            MyHeight = 691
        End Sub

        Public Sub SetToDSC()
            MyLat = -(30 + (31 / 60) + (34.7 / 3600))
            MyLong = -(70 + (54 / 60) + (11.8 / 3600))
            MyHeight = 1700
        End Sub

        '''<summary>Get the current observer position.</summary>
        Public Function GetObserver() As ASCOM.Tools.Novas31.OnSurface
            Dim RetVal As New ASCOM.Tools.Novas31.OnSurface
            With RetVal
                .Temperature = MyTemperature
                .Pressure = MyPressure
                .Latitude = MyLat
                .Longitude = MyLong
                .Height = MyHeight
            End With
            Return RetVal
        End Function

    End Class

End Class
