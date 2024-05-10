Option Explicit On
Option Strict On

Public Class cDB

    Public novas As New ASCOM.Tools.Novas31.Novas

    Public Properties As New cProperties

    Public Class cProperties

        <ComponentModel.Category("1. Parameters")>
        <ComponentModel.DisplayName("1. Right Ascension")>
        Public Property RightAscension As String
            Get
                Return MyRA
            End Get
            Set(value As String)
                MyRA = value
            End Set
        End Property
        Private MyRA As String = String.Empty

        <ComponentModel.Category("1. Parameters")>
        <ComponentModel.DisplayName("2. Declination")>
        Public Property Declination As String
            Get
                Return MyDec
            End Get
            Set(value As String)
                MyDec = value
            End Set
        End Property
        Private MyDec As String = String.Empty

        <ComponentModel.Category("1. Parameters")>
        <ComponentModel.DisplayName("3. Latitude")>
        <ComponentModel.Description("Geodetic (ITRS) latitude; north positive (degrees)")>
        Public Property Latitude As String
            Get
                Return MyLatitude
            End Get
            Set(value As String)
                MyLatitude = value
            End Set
        End Property
        Private MyLatitude As String = String.Empty

        <ComponentModel.Category("1. Parameters")>
        <ComponentModel.DisplayName("4. Longitude")>
        <ComponentModel.Description("Geodetic (ITRS) longitude; east positive (degrees)")>
        Public Property Longitude As String
            Get
                Return MyLongitude
            End Get
            Set(value As String)
                MyLongitude = value
            End Set
        End Property
        Private MyLongitude As String = String.Empty

        <ComponentModel.Category("1. Parameters")>
        <ComponentModel.DisplayName("5. Height")>
        Public Property Heigth As Double
            Get
                Return MyHeight
            End Get
            Set(value As Double)
                MyHeight = value
            End Set
        End Property
        Private MyHeight As Double = 0

        <ComponentModel.Category("1. Parameters")>
        <ComponentModel.DisplayName("6. Temperature")>
        <ComponentModel.Description("Observer's location's ambient temperature (degrees Celsius)")>
        Public Property Temperature As Double
            Get
                Return MyTemperature
            End Get
            Set(value As Double)
                MyTemperature = value
            End Set
        End Property
        Private MyTemperature As Double = 0

        <ComponentModel.Category("1. Parameters")>
        <ComponentModel.DisplayName("7. Pressure")>
        <ComponentModel.Description("Observer's location's atmospheric pressure (millibars)")>
        Public Property Pressure As Double
            Get
                Return MyPressure
            End Get
            Set(value As Double)
                MyPressure = value
            End Set
        End Property
        Private MyPressure As Double = 1013.0

        '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

        <ComponentModel.Category("2. Calculation")>
        <ComponentModel.DisplayName("1. Start date and time")>
        Public Property UTC_Start As DateTime
            Get
                Return MyUTC_Start
            End Get
            Set(value As DateTime)
                MyUTC_Start = value
            End Set
        End Property
        Private MyUTC_Start As DateTime = Now.ToUniversalTime

        <ComponentModel.Category("2. Calculation")>
        <ComponentModel.DisplayName("2. Calculation duration")>
        Public Property UTC_Range As TimeSpan
            Get
                Return MyUTC_Range
            End Get
            Set(value As TimeSpan)
                MyUTC_Range = value
            End Set
        End Property
        Private MyUTC_Range As TimeSpan = New TimeSpan(7, 0, 0, 0)

        <ComponentModel.Category("2. Calculation")>
        <ComponentModel.DisplayName("3. Calculation stepping")>
        Public Property UTC_Stepping As TimeSpan
            Get
                Return MyUTC_Stepping
            End Get
            Set(value As TimeSpan)
                MyUTC_Stepping = value
            End Set
        End Property
        Private MyUTC_Stepping As TimeSpan = New TimeSpan(0, 1, 0)

        '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

        <ComponentModel.Category("3. Limits")>
        <ComponentModel.DisplayName("1. Minimum heigth above horizon")>
        Public Property Limit_MinHeigth As Double
            Get
                Return MyLimit_MinHeigth
            End Get
            Set(value As Double)
                MyLimit_MinHeigth = value
            End Set
        End Property
        Private MyLimit_MinHeigth As Double = 5.0

        <ComponentModel.Category("3. Limits")>
        <ComponentModel.DisplayName("2. Maximum sun heigth")>
        Public Property Limit_MaxSunHeigth As Double
            Get
                Return MyLimit_MaxSunHeigth
            End Get
            Set(value As Double)
                MyLimit_MaxSunHeigth = value
            End Set
        End Property
        Private MyLimit_MaxSunHeigth As Double = -6.0

        '══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════

        '''<summary>Get the current observer position.</summary>
        Public Function GetObserver() As ASCOM.Tools.Novas31.OnSurface
            Dim RetVal As New ASCOM.Tools.Novas31.OnSurface
            With RetVal
                .Temperature = Temperature
                .Pressure = Pressure
                .Latitude = GetLocation.Latitude
                .Longitude = GetLocation.Longitude
                .Height = MyHeight
            End With
            Return RetVal
        End Function

        Public Function GetLocation() As Ato.AstroCalc.sLatLong
            Return New Ato.AstroCalc.sLatLong(Latitude, Longitude)
        End Function

        Public Function GetJ2000() As Ato.AstroCalc.sRADec
            Return New Ato.AstroCalc.sRADec(RightAscension, Declination)
        End Function

    End Class

End Class
