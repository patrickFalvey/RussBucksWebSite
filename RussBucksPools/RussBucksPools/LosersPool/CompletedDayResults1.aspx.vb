﻿Imports System.Data

Imports System
Imports System.Collections.Specialized
Imports System.Collections
Imports System.ComponentModel
Imports System.Security.Permissions
Imports System.Web

Imports System.Web.UI
Imports System.Web.UI.WebControls


Imports RussBucksPools
Imports RussBucksPools.LosersPool.Models
Imports RussBucksPools.JoinPools.Models


Public Class CompletedDayResults
    Inherits System.Web.UI.Page

    Private GameUpdateCollection As New Dictionary(Of String, GameUpdate)
    Private GameUpdateCollectionSorted As New Dictionary(Of String, GameUpdate)


    ' Keeps track of if users are winning, losing, of if the game they picked is tied
    Private UserStatusCollection As New Dictionary(Of String, UserStatus)


    Private WinningTeamForeColor As System.Drawing.Color = Drawing.Color.Green
    Private WinningTeamBackColor As System.Drawing.Color = Drawing.Color.LightGreen

    Private LosingTeamForeColor As System.Drawing.Color = Drawing.Color.Red
    Private LosingTeamBackColor As System.Drawing.Color = Drawing.Color.LightSalmon

    Private userColor As New System.Drawing.Color

    Private UserForeColor As System.Drawing.Color = Drawing.Color.Green
    Private UserBackColor As System.Drawing.Color = Drawing.Color.LightGreen


    Private AvailableForeColor As System.Drawing.Color = Drawing.Color.Green
    Private AvailableBackColor As System.Drawing.Color = Drawing.Color.LightGreen

    Private NotAvailableForeColor As System.Drawing.Color = Drawing.Color.Red
    Private NotAvailableBackColor As System.Drawing.Color = Drawing.Color.LightSalmon


    Private TeamBackColor As System.Drawing.Color = Drawing.Color.White

    Private LoserCollectionSorted As New Dictionary(Of String, Loser)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("userId") Is Nothing Then
            Response.Redirect("~/Account/Login.aspx")
        End If

        Dim thisDay = CStr(Session("completedDayId"))

        Dim _DbLoserPool As New LosersPoolContext
        Dim _DbPools2 As New PoolDbContext

        Try
            Using (_DbLoserPool)
                Using (_DbPools2)

                    Dim queryContenders As New List(Of UserChoices)

                    queryContenders = (From contender1 In _DbLoserPool.UserChoicesList
                                       Where contender1.DayId = thisDay And contender1.UserPick <> "").ToList


                    Dim teams1 As New List(Of Team)

                    teams1 = (From teams2 In _DbPools2.Teams).ToList

                    Dim dailyGames As New List(Of ScheduleEntity)

                    dailyGames = (From scheduleEntity1 In _DbLoserPool.ScheduleEntities
                           Where scheduleEntity1.DayId = thisDay
                           Select scheduleEntity1).ToList

                    Dim cnt1 = 0
                    For Each game1 In dailyGames

                        cnt1 = cnt1 + 1
                        Dim gameupdate1 As New GameUpdate

                        gameupdate1.GameId = "game" + CStr(cnt1)
                        gameupdate1.HomeTeam = game1.HomeTeam
                        gameupdate1.AwayTeam = game1.AwayTeam
                        gameupdate1.HomeScore = game1.HomeScore
                        gameupdate1.AwayScore = game1.AwayScore
                        gameupdate1.GameTime = game1.GameTime



                        For Each user1 In queryContenders


                            gameupdate1.UserHandles.Add(user1.UserName)

                            Dim teamAvailability As String

                            For Each team1 In teams1

                                If team1.TeamName = game1.HomeTeam Then
                                    If team1.TeamName = "Washington" Then
                                        teamAvailability = user1.Washington
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Miami" Then
                                        teamAvailability = user1.Miami
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Colorado" Then
                                        teamAvailability = user1.Colorado
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Arizona" Then
                                        teamAvailability = user1.Arizona
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "San Francisco" Then
                                        teamAvailability = user1.SanFrancisco
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "San Diego" Then
                                        teamAvailability = user1.SanDiego
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Pittsburg" Then
                                        teamAvailability = user1.Pittsburg
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Cincinnati" Then
                                        teamAvailability = user1.Cincinnati
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Toronto" Then
                                        teamAvailability = user1.Toronto
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "NY Yankees" Then
                                        teamAvailability = user1.NYYankees
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Boston" Then
                                        teamAvailability = user1.Boston
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Tampa Bay" Then
                                        teamAvailability = user1.TampaBay
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Atlanta" Then
                                        teamAvailability = user1.Atlanta
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Philadelphia" Then
                                        teamAvailability = user1.Philadelphia
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Chicago White Sox" Then
                                        teamAvailability = user1.ChicagoWhiteSox
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Detroit" Then
                                        teamAvailability = user1.Detroit
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Kansas City" Then
                                        teamAvailability = user1.KansasCity
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Cleveland" Then
                                        teamAvailability = user1.Cleveland
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Milwaukee" Then
                                        teamAvailability = user1.Milwaukee
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "LA Dodgers" Then
                                        teamAvailability = user1.LADodgers
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Minnesota" Then
                                        teamAvailability = user1.Minnesota
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Oakland" Then
                                        teamAvailability = user1.Oakland
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Houston" Then
                                        teamAvailability = user1.Houston
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Texas" Then
                                        teamAvailability = user1.Texas
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "St. Louis" Then
                                        teamAvailability = user1.STLouis
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Chicago Cubs" Then
                                        teamAvailability = user1.ChicagoCubs
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "LA Angels" Then
                                        teamAvailability = user1.LAAngels
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Seattle" Then
                                        teamAvailability = user1.Seattle
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "NY Mets" Then
                                        teamAvailability = user1.NYMets
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Baltimore" Then
                                        teamAvailability = user1.Baltimore
                                        gameupdate1 = SetHomeTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    End If

                                ElseIf team1.TeamName = game1.AwayTeam Then

                                    If team1.TeamName = "Washington" Then
                                        teamAvailability = user1.Washington
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Miami" Then
                                        teamAvailability = user1.Miami
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Colorado" Then
                                        teamAvailability = user1.Colorado
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Arizona" Then
                                        teamAvailability = user1.Arizona
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "San Francisco" Then
                                        teamAvailability = user1.SanFrancisco
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "San Diego" Then
                                        teamAvailability = user1.SanDiego
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Pittsburg" Then
                                        teamAvailability = user1.Pittsburg
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Cincinnati" Then
                                        teamAvailability = user1.Cincinnati
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Toronto" Then
                                        teamAvailability = user1.Toronto
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "NY Yankees" Then
                                        teamAvailability = user1.NYYankees
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Boston" Then
                                        teamAvailability = user1.Boston
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Tampa Bay" Then
                                        teamAvailability = user1.TampaBay
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Atlanta" Then
                                        teamAvailability = user1.Atlanta
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Philadelphia" Then
                                        teamAvailability = user1.Philadelphia
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Chicago White Sox" Then
                                        teamAvailability = user1.ChicagoWhiteSox
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Detroit" Then
                                        teamAvailability = user1.Detroit
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Kansas City" Then
                                        teamAvailability = user1.KansasCity
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Cleveland" Then
                                        teamAvailability = user1.Cleveland
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Milwaukee" Then
                                        teamAvailability = user1.Milwaukee
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "LA Dodgers" Then
                                        teamAvailability = user1.LADodgers
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Minnesota" Then
                                        teamAvailability = user1.Minnesota
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Oakland" Then
                                        teamAvailability = user1.Oakland
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Houston" Then
                                        teamAvailability = user1.Houston
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Texas" Then
                                        teamAvailability = user1.Texas
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "St. Louis" Then
                                        teamAvailability = user1.STLouis
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Chicago Cubs" Then
                                        teamAvailability = user1.ChicagoCubs
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "LA Angels" Then
                                        teamAvailability = user1.LAAngels
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Seattle" Then
                                        teamAvailability = user1.Seattle
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "NY Mets" Then
                                        teamAvailability = user1.NYMets
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    ElseIf team1.TeamName = "Baltimore" Then
                                        teamAvailability = user1.Baltimore
                                        gameupdate1 = SetAwayTeamAvailabilityState(gameupdate1, team1.TeamName, teamAvailability, user1)
                                    End If

                                End If

                            Next
                        Next
                        GameUpdateCollection.Add(gameupdate1.GameId, gameupdate1)
                    Next


                    Dim queryGameUpdateCollection = (From gameUpdateCollection1 In GameUpdateCollection).ToList

                    For Each game In queryGameUpdateCollection

                        Dim HomeTeamImage As String = ""
                        Dim AwayTeamImage As String = ""
                        Dim HomeTeamIcon As String = ""
                        Dim AwayTeamIcon As String = ""

                        If GameUpdateCollection(game.Key).HomeTeam = "Washington" Then
                            HomeTeamImage = "~/MLB ICONS/Washington Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Washington.png"
                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Miami" Then
                            HomeTeamImage = "~/MLB ICONS/Miami Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Miami.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Colorado" Then
                            HomeTeamImage = "~/MLB ICONS/Colorado Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Colorado.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Arizona" Then
                            HomeTeamImage = "~/MLB ICONS/Arizona Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Arizona.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "San Francisco" Then
                            HomeTeamImage = "~/MLB ICONS/San Francisco Label.png"
                            HomeTeamIcon = "~/MLB ICONS/San Francisco.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "San Diego" Then
                            HomeTeamImage = "~/MLB ICONS/San Diego Label.png"
                            HomeTeamIcon = "~/MLB ICONS/San Diego.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Pittsburg" Then
                            HomeTeamImage = "~/MLB ICONS/Pittsburg Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Pittsburg.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Cincinnati" Then
                            HomeTeamImage = "~/MLB ICONS/Cincinnati Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Cincinnati.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Toronto" Then
                            HomeTeamImage = "~/MLB ICONS/Toronto Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Toronto.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "NY Yankees" Then
                            HomeTeamImage = "~/MLB ICONS/NY Yankees Label.png"
                            HomeTeamIcon = "~/MLB ICONS/NY Yankees.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Boston" Then
                            HomeTeamImage = "~/MLB ICONS/Boston Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Boston.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Tampa Bay" Then
                            HomeTeamImage = "~/MLB ICONS/Tampa Bay Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Tampa Bay.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Atlanta" Then
                            HomeTeamImage = "~/MLB ICONS/Atlanta Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Atlanta.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Philadelphia" Then
                            HomeTeamImage = "~/MLB ICONS/Philadelphia Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Philadelphia.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Chicago White Sox" Then
                            HomeTeamImage = "~/MLB ICONS/Chicago White Sox Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Chicago White Sox.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Detroit" Then
                            HomeTeamImage = "~/MLB ICONS/Detroit Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Detroit.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Kansas City" Then
                            HomeTeamImage = "~/MLB ICONS/Kansas City Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Kansas City.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Cleveland" Then
                            HomeTeamImage = "~/MLB ICONS/Cleveland Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Cleveland.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Milwaukee" Then
                            HomeTeamImage = "~/MLB ICONS/Milwaukee Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Milwaukee.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "LA Dodgers" Then
                            HomeTeamImage = "~/MLB ICONS/LA Dodgers Label.png"
                            HomeTeamIcon = "~/MLB ICONS/LA Dodgers.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Minnesota" Then
                            HomeTeamImage = "~/MLB ICONS/Minnesota Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Minnesota.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Oakland" Then
                            HomeTeamImage = "~/MLB ICONS/Oakland Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Oakland.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Houston" Then
                            HomeTeamImage = "~/MLB ICONS/Houston Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Houston.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Texas" Then
                            HomeTeamImage = "~/MLB ICONS/Texas Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Texas.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "St. Louis" Then
                            HomeTeamImage = "~/MLB ICONS/St. Louis Label.png"
                            HomeTeamIcon = "~/MLB ICONS/St. Louis.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Chicago Cubs" Then
                            HomeTeamImage = "~/MLB ICONS/Chicago Cubs Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Chicago Cubs.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "LA Angels" Then
                            HomeTeamImage = "~/MLB ICONS/LA Angels Label.png"
                            HomeTeamIcon = "~/MLB ICONS/LA Angels.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Seattle" Then
                            HomeTeamImage = "~/MLB ICONS/Seattle Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Seattle.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "NY Mets" Then
                            HomeTeamImage = "~/MLB ICONS/NY Mets Label.png"
                            HomeTeamIcon = "~/MLB ICONS/NY Mets.png"

                        ElseIf GameUpdateCollection(game.Key).HomeTeam = "Baltimore" Then
                            HomeTeamImage = "~/MLB ICONS/Baltimore Label.png"
                            HomeTeamIcon = "~/MLB ICONS/Baltimore.png"

                        End If

                        If GameUpdateCollection(game.Key).AwayTeam = "Washington" Then
                            AwayTeamImage = "~/MLB ICONS/Washington Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Washington.png"
                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Miami" Then
                            AwayTeamImage = "~/MLB ICONS/Miami Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Miami.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Colorado" Then
                            AwayTeamImage = "~/MLB ICONS/Colorado Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Colorado.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Arizona" Then
                            AwayTeamImage = "~/MLB ICONS/Arizona Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Arizona.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "San Francisco" Then
                            AwayTeamImage = "~/MLB ICONS/San Francisco Label.png"
                            AwayTeamIcon = "~/MLB ICONS/San Francisco.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "San Diego" Then
                            AwayTeamImage = "~/MLB ICONS/San Diego Label.png"
                            AwayTeamIcon = "~/MLB ICONS/San Diego.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Pittsburg" Then
                            AwayTeamImage = "~/MLB ICONS/Pittsburg Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Pittsburg.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Cincinnati" Then
                            AwayTeamImage = "~/MLB ICONS/Cincinnati Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Cincinnati.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Toronto" Then
                            AwayTeamImage = "~/MLB ICONS/Toronto Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Toronto.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "NY Yankees" Then
                            AwayTeamImage = "~/MLB ICONS/NY Yankees Label.png"
                            AwayTeamIcon = "~/MLB ICONS/NY Yankees.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Boston" Then
                            AwayTeamImage = "~/MLB ICONS/Boston Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Boston.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Tampa Bay" Then
                            AwayTeamImage = "~/MLB ICONS/Tampa Bay Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Tampa Bay.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Atlanta" Then
                            AwayTeamImage = "~/MLB ICONS/Atlanta Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Atlanta.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Philadelphia" Then
                            AwayTeamImage = "~/MLB ICONS/Philadelphia Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Philadelphia.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Chicago White Sox" Then
                            AwayTeamImage = "~/MLB ICONS/Chicago White Sox Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Chicago White Sox.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Detroit" Then
                            AwayTeamImage = "~/MLB ICONS/Detroit Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Detroit.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Kansas City" Then
                            AwayTeamImage = "~/MLB ICONS/Kansas City Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Kansas City.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Cleveland" Then
                            AwayTeamImage = "~/MLB ICONS/Cleveland Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Cleveland.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Milwaukee" Then
                            AwayTeamImage = "~/MLB ICONS/Milwaukee Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Milwaukee.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "LA Dodgers" Then
                            AwayTeamImage = "~/MLB ICONS/LA Dodgers Label.png"
                            AwayTeamIcon = "~/MLB ICONS/LA Dodgers.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Minnesota" Then
                            AwayTeamImage = "~/MLB ICONS/Minnesota Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Minnesota.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Oakland" Then
                            AwayTeamImage = "~/MLB ICONS/Oakland Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Oakland.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Houston" Then
                            AwayTeamImage = "~/MLB ICONS/Houston Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Houston.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Texas" Then
                            AwayTeamImage = "~/MLB ICONS/Texas Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Texas.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "St. Louis" Then
                            AwayTeamImage = "~/MLB ICONS/St. Louis Label.png"
                            AwayTeamIcon = "~/MLB ICONS/St. Louis.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Chicago Cubs" Then
                            AwayTeamImage = "~/MLB ICONS/Chicago Cubs Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Chicago Cubs.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "LA Angels" Then
                            AwayTeamImage = "~/MLB ICONS/LA Angels Label.png"
                            AwayTeamIcon = "~/MLB ICONS/LA Angels.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Seattle" Then
                            AwayTeamImage = "~/MLB ICONS/Seattle Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Seattle.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "NY Mets" Then
                            AwayTeamImage = "~/MLB ICONS/NY Mets Label.png"
                            AwayTeamIcon = "~/MLB ICONS/NY Mets.png"

                        ElseIf GameUpdateCollection(game.Key).AwayTeam = "Baltimore" Then
                            AwayTeamImage = "~/MLB ICONS/Baltimore Label.png"
                            AwayTeamIcon = "~/MLB ICONS/Baltimore.png"


                        End If


                        If game.Key = "game1" Then
                            GameNumber1.Text = game.Key
                            HomeTeam1Image1.ImageUrl = HomeTeamImage
                            AwayTeam1Image1.ImageUrl = AwayTeamImage
                            HomeTeam1Icon1.ImageUrl = HomeTeamIcon
                            AwayTeam1Icon1.ImageUrl = AwayTeamIcon

                            HomeScore1.Text = GameUpdateCollection(game.Key).HomeScore
                            AwayScore1.Text = GameUpdateCollection(game.Key).AwayScore
                            GameNumber1Status.Text = GameUpdateCollection(game.Key).GameTime

                            If CInt(HomeScore1.Text) > CInt(AwayScore1.Text) Then
                                HomeScore1.ForeColor = WinningTeamForeColor
                                AwayScore1.ForeColor = LosingTeamForeColor

                                HomeTeam1Image.BackColor = WinningTeamBackColor
                                AwayTeam1Image.BackColor = LosingTeamBackColor
                                HomeScore1.BackColor = WinningTeamBackColor
                                AwayScore1.BackColor = LosingTeamBackColor
                            ElseIf CInt(AwayScore1.Text) > CInt(HomeScore1.Text) Then
                                HomeScore1.ForeColor = LosingTeamForeColor
                                AwayScore1.ForeColor = WinningTeamForeColor

                                HomeTeam1Image.BackColor = LosingTeamBackColor
                                AwayTeam1Image.BackColor = WinningTeamBackColor
                                HomeScore1.BackColor = LosingTeamBackColor
                                AwayScore1.BackColor = WinningTeamBackColor

                            End If

                        ElseIf game.Key = "game2" Then
                            GameNumber2.Text = game.Key
                            HomeTeam2Image1.ImageUrl = HomeTeamImage
                            AwayTeam2Image1.ImageUrl = AwayTeamImage
                            HomeTeam2Icon1.ImageUrl = HomeTeamIcon
                            AwayTeam2Icon1.ImageUrl = AwayTeamIcon

                            HomeScore2.Text = GameUpdateCollection(game.Key).HomeScore
                            AwayScore2.Text = GameUpdateCollection(game.Key).AwayScore
                            GameNumber2Status.Text = GameUpdateCollection(game.Key).GameTime

                            If CInt(HomeScore2.Text) > CInt(AwayScore2.Text) Then
                                HomeScore2.ForeColor = WinningTeamForeColor
                                AwayScore2.ForeColor = LosingTeamForeColor

                                HomeTeam2Image.BackColor = WinningTeamBackColor
                                AwayTeam2Image.BackColor = LosingTeamBackColor
                                HomeScore2.BackColor = WinningTeamBackColor
                                AwayScore2.BackColor = LosingTeamBackColor
                            ElseIf CInt(AwayScore2.Text) > CInt(HomeScore2.Text) Then
                                HomeScore2.ForeColor = LosingTeamForeColor
                                AwayScore2.ForeColor = WinningTeamForeColor

                                HomeTeam2Image.BackColor = LosingTeamBackColor
                                AwayTeam2Image.BackColor = WinningTeamBackColor
                                HomeScore2.BackColor = LosingTeamBackColor
                                AwayScore2.BackColor = WinningTeamBackColor

                            End If


                        ElseIf game.Key = "game3" Then
                            GameNumber3.Text = game.Key
                            HomeTeam3Image1.ImageUrl = HomeTeamImage
                            AwayTeam3Image1.ImageUrl = AwayTeamImage
                            HomeTeam3Icon1.ImageUrl = HomeTeamIcon
                            AwayTeam3Icon1.ImageUrl = AwayTeamIcon
                            HomeScore3.Text = GameUpdateCollection(game.Key).HomeScore
                            AwayScore3.Text = GameUpdateCollection(game.Key).AwayScore
                            GameNumber3Status.Text = GameUpdateCollection(game.Key).GameTime

                            If CInt(HomeScore3.Text) > CInt(AwayScore3.Text) Then
                                HomeScore3.ForeColor = WinningTeamForeColor
                                AwayScore3.ForeColor = LosingTeamForeColor

                                HomeTeam3Image.BackColor = WinningTeamBackColor
                                AwayTeam3Image.BackColor = LosingTeamBackColor
                                HomeScore3.BackColor = WinningTeamBackColor
                                AwayScore3.BackColor = LosingTeamBackColor
                            ElseIf CInt(AwayScore3.Text) > CInt(HomeScore3.Text) Then
                                HomeScore3.ForeColor = LosingTeamForeColor
                                AwayScore3.ForeColor = WinningTeamForeColor

                                HomeTeam3Image.BackColor = LosingTeamBackColor
                                AwayTeam3Image.BackColor = WinningTeamBackColor
                                HomeScore3.BackColor = LosingTeamBackColor
                                AwayScore3.BackColor = WinningTeamBackColor

                            End If


                        ElseIf game.Key = "game4" Then
                            GameNumber4.Text = game.Key
                            HomeTeam4Image1.ImageUrl = HomeTeamImage
                            AwayTeam4Image1.ImageUrl = AwayTeamImage
                            HomeTeam4Icon1.ImageUrl = HomeTeamIcon
                            AwayTeam4Icon1.ImageUrl = AwayTeamIcon
                            HomeScore4.Text = GameUpdateCollection(game.Key).HomeScore
                            AwayScore4.Text = GameUpdateCollection(game.Key).AwayScore
                            GameNumber4Status.Text = GameUpdateCollection(game.Key).GameTime

                            If CInt(HomeScore4.Text) > CInt(AwayScore4.Text) Then
                                HomeScore4.ForeColor = WinningTeamForeColor
                                AwayScore4.ForeColor = LosingTeamForeColor

                                HomeTeam4Image.BackColor = WinningTeamBackColor
                                AwayTeam4Image.BackColor = LosingTeamBackColor
                                HomeScore4.BackColor = WinningTeamBackColor
                                AwayScore4.BackColor = LosingTeamBackColor
                            ElseIf CInt(AwayScore4.Text) > CInt(HomeScore4.Text) Then
                                HomeScore4.ForeColor = LosingTeamForeColor
                                AwayScore4.ForeColor = WinningTeamForeColor

                                HomeTeam4Image.BackColor = LosingTeamBackColor
                                AwayTeam4Image.BackColor = WinningTeamBackColor
                                HomeScore4.BackColor = LosingTeamBackColor
                                AwayScore4.BackColor = WinningTeamBackColor

                            End If
                        ElseIf game.Key = "game5" Then
                            GameNumber5.Text = game.Key
                            HomeTeam5Image1.ImageUrl = HomeTeamImage
                            AwayTeam5Image1.ImageUrl = AwayTeamImage
                            HomeTeam5Icon1.ImageUrl = HomeTeamIcon
                            AwayTeam5Icon1.ImageUrl = AwayTeamIcon
                            HomeScore5.Text = GameUpdateCollection(game.Key).HomeScore
                            AwayScore5.Text = GameUpdateCollection(game.Key).AwayScore
                            GameNumber5Status.Text = GameUpdateCollection(game.Key).GameTime

                            If CInt(HomeScore5.Text) > CInt(AwayScore5.Text) Then
                                HomeScore5.ForeColor = WinningTeamForeColor
                                AwayScore5.ForeColor = LosingTeamForeColor

                                HomeTeam5Image.BackColor = WinningTeamBackColor
                                AwayTeam5Image.BackColor = LosingTeamBackColor
                                HomeScore5.BackColor = WinningTeamBackColor
                                AwayScore5.BackColor = LosingTeamBackColor
                            ElseIf CInt(AwayScore5.Text) > CInt(HomeScore5.Text) Then
                                HomeScore5.ForeColor = LosingTeamForeColor
                                AwayScore5.ForeColor = WinningTeamForeColor

                                HomeTeam5Image.BackColor = LosingTeamBackColor
                                AwayTeam5Image.BackColor = WinningTeamBackColor
                                HomeScore5.BackColor = LosingTeamBackColor
                                AwayScore5.BackColor = WinningTeamBackColor

                            End If

                        ElseIf game.Key = "game6" Then
                            GameNumber6.Text = game.Key
                            HomeTeam6Image1.ImageUrl = HomeTeamImage
                            AwayTeam6Image1.ImageUrl = AwayTeamImage
                            HomeTeam6Icon1.ImageUrl = HomeTeamIcon
                            AwayTeam6Icon1.ImageUrl = AwayTeamIcon
                            HomeScore6.Text = GameUpdateCollection(game.Key).HomeScore
                            AwayScore6.Text = GameUpdateCollection(game.Key).AwayScore
                            GameNumber6Status.Text = GameUpdateCollection(game.Key).GameTime

                            If CInt(HomeScore6.Text) > CInt(AwayScore6.Text) Then
                                HomeScore6.ForeColor = WinningTeamForeColor
                                AwayScore6.ForeColor = LosingTeamForeColor

                                HomeTeam6Image.BackColor = WinningTeamBackColor
                                AwayTeam6Image.BackColor = LosingTeamBackColor
                                HomeScore6.BackColor = WinningTeamBackColor
                                AwayScore6.BackColor = LosingTeamBackColor
                            ElseIf CInt(AwayScore6.Text) > CInt(HomeScore6.Text) Then
                                HomeScore6.ForeColor = LosingTeamForeColor
                                AwayScore6.ForeColor = WinningTeamForeColor

                                HomeTeam6Image.BackColor = LosingTeamBackColor
                                AwayTeam6Image.BackColor = WinningTeamBackColor
                                HomeScore6.BackColor = LosingTeamBackColor
                                AwayScore6.BackColor = WinningTeamBackColor

                            End If

                        ElseIf game.Key = "game7" Then
                            GameNumber7.Text = game.Key
                            HomeTeam7Image1.ImageUrl = HomeTeamImage
                            AwayTeam7Image1.ImageUrl = AwayTeamImage
                            HomeTeam7Icon1.ImageUrl = HomeTeamIcon
                            AwayTeam7Icon1.ImageUrl = AwayTeamIcon
                            HomeScore7.Text = GameUpdateCollection(game.Key).HomeScore
                            AwayScore7.Text = GameUpdateCollection(game.Key).AwayScore
                            GameNumber7Status.Text = GameUpdateCollection(game.Key).GameTime

                            If CInt(HomeScore7.Text) > CInt(AwayScore7.Text) Then
                                HomeScore7.ForeColor = WinningTeamForeColor
                                AwayScore7.ForeColor = LosingTeamForeColor

                                HomeTeam7Image.BackColor = WinningTeamBackColor
                                AwayTeam7Image.BackColor = LosingTeamBackColor
                                HomeScore7.BackColor = WinningTeamBackColor
                                AwayScore7.BackColor = LosingTeamBackColor
                            ElseIf CInt(AwayScore7.Text) > CInt(HomeScore7.Text) Then
                                HomeScore7.ForeColor = LosingTeamForeColor
                                AwayScore7.ForeColor = WinningTeamForeColor

                                HomeTeam7Image.BackColor = LosingTeamBackColor
                                AwayTeam7Image.BackColor = WinningTeamBackColor
                                HomeScore7.BackColor = LosingTeamBackColor
                                AwayScore7.BackColor = WinningTeamBackColor

                            End If


                        ElseIf game.Key = "game8" Then
                            GameNumber8.Text = game.Key
                            HomeTeam8Image1.ImageUrl = HomeTeamImage
                            AwayTeam8Image1.ImageUrl = AwayTeamImage
                            HomeTeam8Icon1.ImageUrl = HomeTeamIcon
                            AwayTeam8Icon1.ImageUrl = AwayTeamIcon
                            HomeScore8.Text = GameUpdateCollection(game.Key).HomeScore
                            AwayScore8.Text = GameUpdateCollection(game.Key).AwayScore
                            GameNumber8Status.Text = GameUpdateCollection(game.Key).GameTime

                            If CInt(HomeScore8.Text) > CInt(AwayScore8.Text) Then
                                HomeScore8.ForeColor = WinningTeamForeColor
                                AwayScore8.ForeColor = LosingTeamForeColor

                                HomeTeam8Image.BackColor = WinningTeamBackColor
                                AwayTeam8Image.BackColor = LosingTeamBackColor
                                HomeScore8.BackColor = WinningTeamBackColor
                                AwayScore8.BackColor = LosingTeamBackColor
                            ElseIf CInt(AwayScore8.Text) > CInt(HomeScore8.Text) Then
                                HomeScore8.ForeColor = LosingTeamForeColor
                                AwayScore8.ForeColor = WinningTeamForeColor

                                HomeTeam8Image.BackColor = LosingTeamBackColor
                                AwayTeam8Image.BackColor = WinningTeamBackColor
                                HomeScore8.BackColor = LosingTeamBackColor
                                AwayScore8.BackColor = WinningTeamBackColor

                            End If
                        ElseIf game.Key = "game9" Then
                            GameNumber9.Text = game.Key
                            HomeTeam9Image1.ImageUrl = HomeTeamImage
                            AwayTeam9Image1.ImageUrl = AwayTeamImage
                            HomeTeam9Icon1.ImageUrl = HomeTeamIcon
                            AwayTeam9Icon1.ImageUrl = AwayTeamIcon
                            HomeScore9.Text = GameUpdateCollection(game.Key).HomeScore
                            AwayScore9.Text = GameUpdateCollection(game.Key).AwayScore
                            GameNumber9Status.Text = GameUpdateCollection(game.Key).GameTime

                            If CInt(HomeScore9.Text) > CInt(AwayScore9.Text) Then
                                HomeScore9.ForeColor = WinningTeamForeColor
                                AwayScore9.ForeColor = LosingTeamForeColor

                                HomeTeam9Image.BackColor = WinningTeamBackColor
                                AwayTeam9Image.BackColor = LosingTeamBackColor
                                HomeScore9.BackColor = WinningTeamBackColor
                                AwayScore9.BackColor = LosingTeamBackColor
                            ElseIf CInt(AwayScore9.Text) > CInt(HomeScore9.Text) Then
                                HomeScore9.ForeColor = LosingTeamForeColor
                                AwayScore9.ForeColor = WinningTeamForeColor

                                HomeTeam9Image.BackColor = LosingTeamBackColor
                                AwayTeam9Image.BackColor = WinningTeamBackColor
                                HomeScore9.BackColor = LosingTeamBackColor
                                AwayScore9.BackColor = WinningTeamBackColor

                            End If

                        ElseIf game.Key = "game10" Then
                            GameNumber10.Text = game.Key
                            HomeTeam10Image1.ImageUrl = HomeTeamImage
                            AwayTeam10Image1.ImageUrl = AwayTeamImage
                            HomeTeam10Icon1.ImageUrl = HomeTeamIcon
                            AwayTeam10Icon1.ImageUrl = AwayTeamIcon
                            HomeScore10.Text = GameUpdateCollection(game.Key).HomeScore
                            AwayScore10.Text = GameUpdateCollection(game.Key).AwayScore
                            GameNumber10Status.Text = GameUpdateCollection(game.Key).GameTime

                            If CInt(HomeScore10.Text) > CInt(AwayScore10.Text) Then
                                HomeScore10.ForeColor = WinningTeamForeColor
                                AwayScore10.ForeColor = LosingTeamForeColor

                                HomeTeam10Image.BackColor = WinningTeamBackColor
                                AwayTeam10Image.BackColor = LosingTeamBackColor
                                HomeScore10.BackColor = WinningTeamBackColor
                                AwayScore10.BackColor = LosingTeamBackColor
                            ElseIf CInt(AwayScore10.Text) > CInt(HomeScore10.Text) Then
                                HomeScore10.ForeColor = LosingTeamForeColor
                                AwayScore10.ForeColor = WinningTeamForeColor

                                HomeTeam10Image.BackColor = LosingTeamBackColor
                                AwayTeam10Image.BackColor = WinningTeamBackColor
                                HomeScore10.BackColor = LosingTeamBackColor
                                AwayScore10.BackColor = WinningTeamBackColor

                            End If


                        ElseIf game.Key = "game11" Then
                            GameNumber11.Text = game.Key
                            HomeTeam11Image1.ImageUrl = HomeTeamImage
                            AwayTeam11Image1.ImageUrl = AwayTeamImage
                            HomeTeam11Icon1.ImageUrl = HomeTeamIcon
                            AwayTeam11Icon1.ImageUrl = AwayTeamIcon
                            HomeScore11.Text = GameUpdateCollection(game.Key).HomeScore
                            AwayScore11.Text = GameUpdateCollection(game.Key).AwayScore
                            GameNumber11Status.Text = GameUpdateCollection(game.Key).GameTime

                            If CInt(HomeScore11.Text) > CInt(AwayScore11.Text) Then
                                HomeScore11.ForeColor = WinningTeamForeColor
                                AwayScore11.ForeColor = LosingTeamForeColor

                                HomeTeam11Image.BackColor = WinningTeamBackColor
                                AwayTeam11Image.BackColor = LosingTeamBackColor
                                HomeScore11.BackColor = WinningTeamBackColor
                                AwayScore11.BackColor = LosingTeamBackColor
                            ElseIf CInt(AwayScore11.Text) > CInt(HomeScore11.Text) Then
                                HomeScore11.ForeColor = LosingTeamForeColor
                                AwayScore11.ForeColor = WinningTeamForeColor

                                HomeTeam11Image.BackColor = LosingTeamBackColor
                                AwayTeam11Image.BackColor = WinningTeamBackColor
                                HomeScore11.BackColor = LosingTeamBackColor
                                AwayScore11.BackColor = WinningTeamBackColor

                            End If


                        ElseIf game.Key = "game12" Then
                            GameNumber12.Text = game.Key
                            HomeTeam12Image1.ImageUrl = HomeTeamImage
                            AwayTeam12Image1.ImageUrl = AwayTeamImage
                            HomeTeam12Icon1.ImageUrl = HomeTeamIcon
                            AwayTeam12Icon1.ImageUrl = AwayTeamIcon
                            HomeScore12.Text = GameUpdateCollection(game.Key).HomeScore
                            AwayScore12.Text = GameUpdateCollection(game.Key).AwayScore
                            GameNumber12Status.Text = GameUpdateCollection(game.Key).GameTime

                            If CInt(HomeScore12.Text) > CInt(AwayScore12.Text) Then
                                HomeScore12.ForeColor = WinningTeamForeColor
                                AwayScore12.ForeColor = LosingTeamForeColor

                                HomeTeam12Image.BackColor = WinningTeamBackColor
                                AwayTeam12Image.BackColor = LosingTeamBackColor
                                HomeScore12.BackColor = WinningTeamBackColor
                                AwayScore12.BackColor = LosingTeamBackColor
                            ElseIf CInt(AwayScore12.Text) > CInt(HomeScore12.Text) Then
                                HomeScore12.ForeColor = LosingTeamForeColor
                                AwayScore12.ForeColor = WinningTeamForeColor

                                HomeTeam12Image.BackColor = LosingTeamBackColor
                                AwayTeam12Image.BackColor = WinningTeamBackColor
                                HomeScore12.BackColor = LosingTeamBackColor
                                AwayScore12.BackColor = WinningTeamBackColor

                            End If
                        ElseIf game.Key = "game13" Then
                            GameNumber13.Text = game.Key
                            HomeTeam13Image1.ImageUrl = HomeTeamImage
                            AwayTeam13Image1.ImageUrl = AwayTeamImage
                            HomeTeam13Icon1.ImageUrl = HomeTeamIcon
                            AwayTeam13Icon1.ImageUrl = AwayTeamIcon
                            HomeScore13.Text = GameUpdateCollection(game.Key).HomeScore
                            AwayScore13.Text = GameUpdateCollection(game.Key).AwayScore
                            GameNumber13Status.Text = GameUpdateCollection(game.Key).GameTime

                            If CInt(HomeScore13.Text) > CInt(AwayScore13.Text) Then
                                HomeScore13.ForeColor = WinningTeamForeColor
                                AwayScore13.ForeColor = LosingTeamForeColor

                                HomeTeam13Image.BackColor = WinningTeamBackColor
                                AwayTeam13Image.BackColor = LosingTeamBackColor
                                HomeScore13.BackColor = WinningTeamBackColor
                                AwayScore13.BackColor = LosingTeamBackColor
                            ElseIf CInt(AwayScore13.Text) > CInt(HomeScore13.Text) Then
                                HomeScore13.ForeColor = LosingTeamForeColor
                                AwayScore13.ForeColor = WinningTeamForeColor

                                HomeTeam13Image.BackColor = LosingTeamBackColor
                                AwayTeam13Image.BackColor = WinningTeamBackColor
                                HomeScore13.BackColor = LosingTeamBackColor
                                AwayScore13.BackColor = WinningTeamBackColor

                            End If

                        ElseIf game.Key = "game14" Then
                            GameNumber14.Text = game.Key
                            HomeTeam14Image1.ImageUrl = HomeTeamImage
                            AwayTeam14Image1.ImageUrl = AwayTeamImage
                            HomeTeam14Icon1.ImageUrl = HomeTeamIcon
                            AwayTeam14Icon1.ImageUrl = AwayTeamIcon
                            HomeScore14.Text = GameUpdateCollection(game.Key).HomeScore
                            AwayScore14.Text = GameUpdateCollection(game.Key).AwayScore
                            GameNumber14Status.Text = GameUpdateCollection(game.Key).GameTime

                            If CInt(HomeScore14.Text) > CInt(AwayScore14.Text) Then
                                HomeScore14.ForeColor = WinningTeamForeColor
                                AwayScore14.ForeColor = LosingTeamForeColor

                                HomeTeam14Image.BackColor = WinningTeamBackColor
                                AwayTeam14Image.BackColor = LosingTeamBackColor
                                HomeScore14.BackColor = WinningTeamBackColor
                                AwayScore14.BackColor = LosingTeamBackColor
                            ElseIf CInt(AwayScore14.Text) > CInt(HomeScore14.Text) Then
                                HomeScore14.ForeColor = LosingTeamForeColor
                                AwayScore14.ForeColor = WinningTeamForeColor

                                HomeTeam14Image.BackColor = LosingTeamBackColor
                                AwayTeam14Image.BackColor = WinningTeamBackColor
                                HomeScore14.BackColor = LosingTeamBackColor
                                AwayScore14.BackColor = WinningTeamBackColor

                            End If


                        ElseIf game.Key = "game15" Then
                            GameNumber15.Text = game.Key
                            HomeTeam15Image1.ImageUrl = HomeTeamImage
                            AwayTeam15Image1.ImageUrl = AwayTeamImage
                            HomeTeam15Icon1.ImageUrl = HomeTeamIcon
                            AwayTeam15Icon1.ImageUrl = AwayTeamIcon
                            HomeScore15.Text = GameUpdateCollection(game.Key).HomeScore
                            AwayScore15.Text = GameUpdateCollection(game.Key).AwayScore
                            GameNumber15Status.Text = GameUpdateCollection(game.Key).GameTime

                            If CInt(HomeScore15.Text) > CInt(AwayScore15.Text) Then
                                HomeScore15.ForeColor = WinningTeamForeColor
                                AwayScore15.ForeColor = LosingTeamForeColor

                                HomeTeam15Image.BackColor = WinningTeamBackColor
                                AwayTeam15Image.BackColor = LosingTeamBackColor
                                HomeScore15.BackColor = WinningTeamBackColor
                                AwayScore15.BackColor = LosingTeamBackColor
                            ElseIf CInt(AwayScore15.Text) > CInt(HomeScore15.Text) Then
                                HomeScore15.ForeColor = LosingTeamForeColor
                                AwayScore15.ForeColor = WinningTeamForeColor

                                HomeTeam15Image.BackColor = LosingTeamBackColor
                                AwayTeam15Image.BackColor = WinningTeamBackColor
                                HomeScore15.BackColor = LosingTeamBackColor
                                AwayScore15.BackColor = WinningTeamBackColor

                            End If



                        End If
                    Next

                    For Each user1 In GameUpdateCollection("game1").UserHandles
                        For Each game In GameUpdateCollection
                            If GameUpdateCollection.Count >= 1 Then
                                If GameUpdateCollection("game1").HomeTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore1.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore1.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If

                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If

                                        Exit For
                                    End If
                                End If

                                If GameUpdateCollection("game1").AwayTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore1.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If

                                        Exit For
                                    ElseIf AwayScore1.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If

                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If

                                End If
                            End If

                            If GameUpdateCollection.Count >= 2 Then
                                If GameUpdateCollection("game2").HomeTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore2.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore2.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                    End If
                                End If
                                If GameUpdateCollection("game2").AwayTeamAvailability(user1) = "L" Then
                                    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore2.ForeColor = WinningTeamForeColor Then
                                        Dim userStatus1 As New UserStatus
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore2.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                    End If
                                End If
                            End If

                            If GameUpdateCollection.Count >= 3 Then
                                If GameUpdateCollection("game3").HomeTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore3.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore3.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                    End If
                                End If

                                If GameUpdateCollection("game3").AwayTeamAvailability(user1) = "L" Then
                                    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore3.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore3.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection.Count >= 4 Then
                                If GameUpdateCollection("game4").HomeTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore4.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore4.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If

                                If GameUpdateCollection("game4").AwayTeamAvailability(user1) = "L" Then
                                    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore4.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore4.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                            If GameUpdateCollection.Count >= 5 Then
                                If GameUpdateCollection("game5").HomeTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore5.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore5.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If

                                If GameUpdateCollection("game5").AwayTeamAvailability(user1) = "L" Then
                                    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore5.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore5.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection.Count >= 6 Then
                                If GameUpdateCollection("game6").HomeTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore6.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore6.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If

                                If GameUpdateCollection("game6").AwayTeamAvailability(user1) = "L" Then
                                    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore6.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore6.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection.Count >= 7 Then
                                If GameUpdateCollection("game7").HomeTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore7.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore7.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If

                                If GameUpdateCollection("game7").AwayTeamAvailability(user1) = "L" Then
                                    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore7.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore7.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection.Count >= 8 Then
                                If GameUpdateCollection("game8").HomeTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore8.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore8.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If

                                If GameUpdateCollection("game8").AwayTeamAvailability(user1) = "L" Then
                                    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore8.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore8.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                            If GameUpdateCollection.Count >= 9 Then
                                If GameUpdateCollection("game9").HomeTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore9.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore9.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If

                                If GameUpdateCollection("game9").AwayTeamAvailability(user1) = "L" Then
                                    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore9.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore9.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection.Count >= 10 Then
                                If GameUpdateCollection("game10").HomeTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore10.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore10.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If

                                If GameUpdateCollection("game10").AwayTeamAvailability(user1) = "L" Then
                                    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore10.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore10.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection.Count >= 11 Then
                                If GameUpdateCollection("game11").HomeTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore11.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore11.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If

                                If GameUpdateCollection("game11").AwayTeamAvailability(user1) = "L" Then
                                    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore11.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore11.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                            If GameUpdateCollection.Count >= 12 Then
                                If GameUpdateCollection("game12").HomeTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore12.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore12.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If

                                If GameUpdateCollection("game12").AwayTeamAvailability(user1) = "L" Then
                                    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore12.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore12.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection.Count >= 13 Then
                                If GameUpdateCollection("game13").HomeTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore13.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore13.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If

                                If GameUpdateCollection("game13").AwayTeamAvailability(user1) = "L" Then
                                    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore13.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore13.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection.Count >= 14 Then
                                If GameUpdateCollection("game14").HomeTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore14.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore14.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                    End If
                                End If

                                If GameUpdateCollection("game14").AwayTeamAvailability(user1) = "L" Then
                                    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore14.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore14.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If

                            If GameUpdateCollection.Count >= 15 Then
                                If GameUpdateCollection("game15").HomeTeamAvailability(user1) = "L" Then
                                    Dim user1Status As New UserStatus
                                    user1Status.UserName = user1
                                    If HomeScore15.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf HomeScore15.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For

                                    End If
                                End If

                                If GameUpdateCollection("game15").AwayTeamAvailability(user1) = "L" Then
                                    Dim user1Status = New UserStatus
                                    user1Status.UserName = user1
                                    If AwayScore15.ForeColor = WinningTeamForeColor Then
                                        user1Status.UserColor = LosingTeamForeColor
                                        user1Status.IsUserWinning = "no"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    ElseIf AwayScore15.ForeColor = LosingTeamForeColor Then
                                        user1Status.UserColor = WinningTeamForeColor
                                        user1Status.IsUserWinning = "yes"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    Else
                                        user1Status.UserColor = Drawing.Color.Black
                                        user1Status.IsUserWinning = "tied"
                                        If UserStatusCollection.ContainsKey(user1) Then
                                        Else
                                            UserStatusCollection.Add(user1, user1Status)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                    Next


                    'Need to Sort List
                    For Each game1 In GameUpdateCollection
                        Dim game2 As New GameUpdate
                        game2.GameId = game1.Value.GameId
                        game2.GameTime = game1.Value.GameTime
                        game2.HomeTeam = game1.Value.HomeTeam
                        game2.HomeScore = game1.Value.HomeScore
                        game2.HomeTeamAvailability = game1.Value.HomeTeamAvailability
                        game2.AwayTeam = game1.Value.AwayTeam
                        game2.AwayScore = game1.Value.AwayScore
                        game2.AwayTeamAvailability = game1.Value.AwayTeamAvailability
                        For Each user1Status In UserStatusCollection
                            If user1Status.Value.IsUserWinning = "yes" Then
                                game2.UserHandles.Add(user1Status.Key)
                            End If
                        Next
                        For Each user1Status In UserStatusCollection
                            If user1Status.Value.IsUserWinning = "tied" Then
                                game2.UserHandles.Add(user1Status.Key)
                            End If
                        Next
                        For Each user1Status In UserStatusCollection
                            If user1Status.Value.IsUserWinning = "no" Then
                                game2.UserHandles.Add(user1Status.Key)
                            End If
                        Next
                        GameUpdateCollectionSorted.Add(game2.GameId, game2)
                    Next




                    Dim cnt = 0
                    For Each user1 In GameUpdateCollectionSorted("game1").UserHandles

                        If GameUpdateCollectionSorted.Count <= 1 Then
                            GameNumber2.Visible = False
                            GameNumber2Status.Visible = False
                            HomeTeam2Image.Visible = False
                            HomeTeam2Icon.Visible = False
                            AwayTeam2Image.Visible = False
                            AwayTeam2Icon.Visible = False
                            HomeScore2.Visible = False
                            AwayScore2.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 2 Then
                            GameNumber3.Visible = False
                            GameNumber3Status.Visible = False
                            HomeTeam3Image.Visible = False
                            HomeTeam3Icon.Visible = False
                            AwayTeam3Image.Visible = False
                            AwayTeam3Icon.Visible = False
                            HomeScore3.Visible = False
                            AwayScore3.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 3 Then
                            GameNumber4.Visible = False
                            GameNumber4Status.Visible = False
                            HomeTeam4Image.Visible = False
                            HomeTeam4Icon.Visible = False
                            AwayTeam4Image.Visible = False
                            AwayTeam4Icon.Visible = False
                            HomeScore4.Visible = False
                            AwayScore4.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 4 Then
                            GameNumber5.Visible = False
                            GameNumber5Status.Visible = False
                            HomeTeam5Image.Visible = False
                            HomeTeam5Icon.Visible = False
                            AwayTeam5Image.Visible = False
                            AwayTeam5Icon.Visible = False
                            HomeScore5.Visible = False
                            AwayScore5.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 5 Then
                            GameNumber6.Visible = False
                            GameNumber6Status.Visible = False
                            HomeTeam6Image.Visible = False
                            HomeTeam6Icon.Visible = False
                            AwayTeam6Image.Visible = False
                            AwayTeam6Icon.Visible = False
                            HomeScore6.Visible = False
                            AwayScore6.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 6 Then
                            GameNumber7.Visible = False
                            GameNumber7Status.Visible = False
                            HomeTeam7Image.Visible = False
                            HomeTeam7Icon.Visible = False
                            AwayTeam7Image.Visible = False
                            AwayTeam7Icon.Visible = False
                            HomeScore7.Visible = False
                            AwayScore7.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 7 Then
                            GameNumber8.Visible = False
                            GameNumber8Status.Visible = False
                            HomeTeam8Image.Visible = False
                            HomeTeam8Icon.Visible = False
                            AwayTeam8Image.Visible = False
                            AwayTeam8Icon.Visible = False
                            HomeScore8.Visible = False
                            AwayScore8.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 8 Then
                            GameNumber9.Visible = False
                            GameNumber9Status.Visible = False
                            HomeTeam9Image.Visible = False
                            HomeTeam9Icon.Visible = False
                            AwayTeam9Image.Visible = False
                            AwayTeam9Icon.Visible = False
                            HomeScore9.Visible = False
                            AwayScore9.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 9 Then
                            GameNumber10.Visible = False
                            GameNumber10Status.Visible = False
                            HomeTeam10Image.Visible = False
                            HomeTeam10Icon.Visible = False
                            AwayTeam10Image.Visible = False
                            AwayTeam10Icon.Visible = False
                            HomeScore10.Visible = False
                            AwayScore10.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 10 Then
                            GameNumber11.Visible = False
                            GameNumber11Status.Visible = False
                            HomeTeam11Image.Visible = False
                            HomeTeam11Icon.Visible = False
                            AwayTeam11Image.Visible = False
                            AwayTeam11Icon.Visible = False
                            HomeScore11.Visible = False
                            AwayScore11.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 11 Then
                            GameNumber12.Visible = False
                            GameNumber12Status.Visible = False
                            HomeTeam12Image.Visible = False
                            HomeTeam12Icon.Visible = False
                            AwayTeam12Image.Visible = False
                            AwayTeam12Icon.Visible = False
                            HomeScore12.Visible = False
                            AwayScore12.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 12 Then
                            GameNumber13.Visible = False
                            GameNumber13Status.Visible = False
                            HomeTeam13Image.Visible = False
                            HomeTeam13Icon.Visible = False
                            AwayTeam13Image.Visible = False
                            AwayTeam13Icon.Visible = False
                            HomeScore13.Visible = False
                            AwayScore13.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 13 Then
                            GameNumber14.Visible = False
                            GameNumber14Status.Visible = False
                            HomeTeam14Image.Visible = False
                            HomeTeam14Icon.Visible = False
                            AwayTeam14Image.Visible = False
                            AwayTeam14Icon.Visible = False
                            HomeScore14.Visible = False
                            AwayScore14.Visible = False
                        End If

                        If GameUpdateCollectionSorted.Count <= 14 Then
                            GameNumber15.Visible = False
                            GameNumber15Status.Visible = False
                            HomeTeam15Image.Visible = False
                            HomeTeam15Icon.Visible = False
                            AwayTeam15Image.Visible = False
                            AwayTeam15Icon.Visible = False
                            HomeScore15.Visible = False
                            AwayScore15.Visible = False
                        End If


                        Dim dRow As New TableRow

                        If GameUpdateCollectionSorted.Count >= 1 Then

                            Dim dCell1 As New TableCell
                            dCell1.Text = GameUpdateCollectionSorted("game1").UserHandles(cnt)
                            dCell1.ForeColor = UserStatusCollection(user1).UserColor
                            dCell1.Font.Bold = True
                            dCell1.CssClass = "UserName1"
                            dRow.Cells.Add(dCell1)


                            Dim dCell2 As New TableCell
                            dCell2.Text = GameUpdateCollectionSorted("game1").HomeTeamAvailability(user1)

                            If dCell2.Text = "L" Then
                                dCell2.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell2.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell2.BackColor = LosingTeamBackColor
                                End If

                                dCell2.Font.Bold = True
                            End If

                            dRow.Cells.Add(dCell2)

                            Dim dCell3 As New TableCell
                            dCell3.Text = GameUpdateCollectionSorted("game1").AwayTeamAvailability(user1)

                            If dCell3.Text = "L" Then
                                dCell3.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell3.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell3.BackColor = LosingTeamBackColor
                                End If

                                dCell3.Font.Bold = True

                            End If

                            dRow.Cells.Add(dCell3)

                        End If

                        If GameUpdateCollectionSorted.Count >= 2 Then
                            Dim dCell4 As New TableCell
                            dCell4.Text = GameUpdateCollectionSorted("game2").HomeTeamAvailability(user1)

                            If dCell4.Text = "L" Then
                                dCell4.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell4.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell4.BackColor = LosingTeamBackColor
                                End If

                                dCell4.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell4)

                            Dim dCell5 As New TableCell
                            dCell5.Text = GameUpdateCollectionSorted("game2").AwayTeamAvailability(user1)

                            If dCell5.Text = "L" Then
                                dCell5.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell5.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell5.BackColor = LosingTeamBackColor
                                End If

                                dCell5.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell5)

                        End If

                        If GameUpdateCollectionSorted.Count >= 3 Then

                            Dim dCell6 As New TableCell
                            dCell6.Text = GameUpdateCollectionSorted("game3").HomeTeamAvailability(user1)

                            If dCell6.Text = "L" Then
                                dCell6.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell6.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell6.BackColor = LosingTeamBackColor
                                End If

                                dCell6.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell6)

                            Dim dCell7 As New TableCell
                            dCell7.Text = GameUpdateCollectionSorted("game3").AwayTeamAvailability(user1)

                            If dCell7.Text = "L" Then
                                dCell7.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell7.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell7.BackColor = LosingTeamBackColor
                                End If

                                dCell7.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell7)

                        End If

                        If GameUpdateCollectionSorted.Count >= 4 Then
                            Dim dCell8 As New TableCell
                            dCell8.Text = GameUpdateCollectionSorted("game4").HomeTeamAvailability(user1)

                            If dCell8.Text = "L" Then
                                dCell8.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell8.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell8.BackColor = LosingTeamBackColor
                                End If

                                dCell8.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell8)

                            Dim dCell9 As New TableCell
                            dCell9.Text = GameUpdateCollectionSorted("game4").AwayTeamAvailability(user1)

                            If dCell9.Text = "L" Then
                                dCell9.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell9.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell9.BackColor = LosingTeamBackColor
                                End If

                                dCell9.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell9)

                        End If

                        If GameUpdateCollectionSorted.Count >= 5 Then


                            Dim dCell10 As New TableCell
                            dCell10.Text = GameUpdateCollectionSorted("game5").HomeTeamAvailability(user1)

                            If dCell10.Text = "L" Then
                                dCell10.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell10.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell10.BackColor = LosingTeamBackColor
                                End If

                                dCell10.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell10)

                            Dim dCell11 As New TableCell
                            dCell11.Text = GameUpdateCollectionSorted("game5").AwayTeamAvailability(user1)

                            If dCell11.Text = "L" Then
                                dCell11.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell11.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell11.BackColor = LosingTeamBackColor
                                End If

                                dCell11.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell11)

                        End If

                        If GameUpdateCollectionSorted.Count >= 6 Then
                            Dim dCell12 As New TableCell
                            dCell12.Text = GameUpdateCollectionSorted("game6").HomeTeamAvailability(user1)

                            If dCell12.Text = "L" Then
                                dCell12.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell12.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell12.BackColor = LosingTeamBackColor
                                End If

                                dCell12.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell12)

                            Dim dCell13 As New TableCell
                            dCell13.Text = GameUpdateCollectionSorted("game6").AwayTeamAvailability(user1)

                            If dCell13.Text = "L" Then
                                dCell13.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell13.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell13.BackColor = LosingTeamBackColor
                                End If

                                dCell13.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell13)

                        End If

                        If GameUpdateCollectionSorted.Count >= 7 Then

                            Dim dCell14 As New TableCell
                            dCell14.Text = GameUpdateCollectionSorted("game7").HomeTeamAvailability(user1)
                            If dCell14.Text = "L" Then
                                dCell14.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell14.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell14.BackColor = LosingTeamBackColor
                                End If

                                dCell14.Font.Bold = True
                            End If

                            dRow.Cells.Add(dCell14)

                            Dim dCell15 As New TableCell
                            dCell15.Text = GameUpdateCollectionSorted("game7").AwayTeamAvailability(user1)

                            If dCell15.Text = "L" Then
                                dCell15.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell15.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell15.BackColor = LosingTeamBackColor
                                End If

                                dCell15.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell15)

                        End If

                        If GameUpdateCollectionSorted.Count >= 8 Then
                            Dim dCell16 As New TableCell
                            dCell16.Text = GameUpdateCollectionSorted("game8").HomeTeamAvailability(user1)

                            If dCell16.Text = "L" Then
                                dCell16.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell16.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell16.BackColor = LosingTeamBackColor
                                End If

                                dCell16.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell16)

                            Dim dCell17 As New TableCell
                            dCell17.Text = GameUpdateCollectionSorted("game8").AwayTeamAvailability(user1)

                            If dCell17.Text = "L" Then
                                dCell17.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell17.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell17.BackColor = LosingTeamBackColor
                                End If

                                dCell17.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell17)

                        End If

                        If GameUpdateCollectionSorted.Count >= 9 Then

                            Dim dCell18 As New TableCell
                            dCell18.Text = GameUpdateCollectionSorted("game9").HomeTeamAvailability(user1)

                            If dCell18.Text = "L" Then
                                dCell18.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell18.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell18.BackColor = LosingTeamBackColor
                                End If

                                dCell18.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell18)

                            Dim dCell19 As New TableCell
                            dCell19.Text = GameUpdateCollectionSorted("game9").AwayTeamAvailability(user1)

                            If dCell19.Text = "L" Then
                                dCell19.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell19.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell19.BackColor = LosingTeamBackColor
                                End If

                                dCell19.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell19)
                        End If

                        If GameUpdateCollectionSorted.Count >= 10 Then
                            Dim dCell20 As New TableCell
                            dCell20.Text = GameUpdateCollectionSorted("game10").HomeTeamAvailability(user1)

                            If dCell20.Text = "L" Then
                                dCell20.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell20.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell20.BackColor = LosingTeamBackColor
                                End If

                                dCell20.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell20)

                            Dim dCell21 As New TableCell
                            dCell21.Text = GameUpdateCollectionSorted("game10").AwayTeamAvailability(user1)

                            If dCell21.Text = "L" Then
                                dCell21.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell21.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell21.BackColor = LosingTeamBackColor
                                End If

                                dCell21.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell21)
                        End If

                        If GameUpdateCollectionSorted.Count >= 11 Then

                            Dim dCell22 As New TableCell
                            dCell22.Text = GameUpdateCollectionSorted("game11").HomeTeamAvailability(user1)

                            If dCell22.Text = "L" Then
                                dCell22.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell22.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell22.BackColor = LosingTeamBackColor
                                End If

                                dCell22.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell22)

                            Dim dCell23 As New TableCell
                            dCell23.Text = GameUpdateCollectionSorted("game11").AwayTeamAvailability(user1)

                            If dCell23.Text = "L" Then
                                dCell23.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell23.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell23.BackColor = LosingTeamBackColor
                                End If

                                dCell23.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell23)

                        End If

                        If GameUpdateCollectionSorted.Count >= 12 Then
                            Dim dCell24 As New TableCell
                            dCell24.Text = GameUpdateCollectionSorted("game12").HomeTeamAvailability(user1)

                            If dCell24.Text = "L" Then
                                dCell24.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell24.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell24.BackColor = LosingTeamBackColor
                                End If

                                dCell24.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell24)

                            Dim dCell25 As New TableCell
                            dCell25.Text = GameUpdateCollectionSorted("game12").AwayTeamAvailability(user1)

                            If dCell25.Text = "L" Then
                                dCell25.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell25.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell25.BackColor = LosingTeamBackColor
                                End If

                                dCell25.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell25)

                        End If

                        If GameUpdateCollectionSorted.Count >= 13 Then

                            Dim dCell26 As New TableCell
                            dCell26.Text = GameUpdateCollectionSorted("game13").HomeTeamAvailability(user1)

                            If dCell26.Text = "L" Then
                                dCell26.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell26.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell26.BackColor = LosingTeamBackColor
                                End If

                                dCell26.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell26)

                            Dim dCell27 As New TableCell
                            dCell27.Text = GameUpdateCollectionSorted("game13").AwayTeamAvailability(user1)

                            If dCell27.Text = "L" Then
                                dCell27.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell27.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell27.BackColor = LosingTeamBackColor
                                End If

                                dCell27.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell27)

                        End If

                        If GameUpdateCollectionSorted.Count >= 14 Then
                            Dim dCell28 As New TableCell
                            dCell28.Text = GameUpdateCollectionSorted("game14").HomeTeamAvailability(user1)

                            If dCell28.Text = "L" Then
                                dCell28.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell28.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell28.BackColor = LosingTeamBackColor
                                End If

                                dCell28.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell28)

                            Dim dCell29 As New TableCell
                            dCell29.Text = GameUpdateCollectionSorted("game14").AwayTeamAvailability(user1)

                            If dCell29.Text = "L" Then
                                dCell29.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell29.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell29.BackColor = LosingTeamBackColor
                                End If

                                dCell29.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell29)

                        End If

                        If GameUpdateCollectionSorted.Count >= 15 Then

                            Dim dCell30 As New TableCell
                            dCell30.Text = GameUpdateCollectionSorted("game15").HomeTeamAvailability(user1)

                            If dCell30.Text = "L" Then
                                dCell30.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell30.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell30.BackColor = LosingTeamBackColor
                                End If

                                dCell30.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell30)

                            Dim dCell31 As New TableCell
                            dCell31.Text = GameUpdateCollectionSorted("game15").AwayTeamAvailability(user1)

                            If dCell31.Text = "L" Then
                                dCell31.ForeColor = UserStatusCollection(user1).UserColor
                                If UserStatusCollection(user1).UserColor = WinningTeamForeColor Then
                                    dCell31.BackColor = WinningTeamBackColor
                                ElseIf UserStatusCollection(user1).UserColor = LosingTeamForeColor Then
                                    dCell31.BackColor = LosingTeamBackColor
                                End If

                                dCell31.Font.Bold = True
                            End If
                            dRow.Cells.Add(dCell31)

                        End If


                        dRow.CssClass = "dRow1"
                        If GameUpdateCollectionSorted.Count >= 1 Then
                            TeamsByGameTable1.Rows.Add(dRow)
                        End If

                        cnt = cnt + 1
                    Next

                    If GameUpdateCollectionSorted.Count >= 1 Then
                        TeamsByGameTable1.DataBind()
                    End If


                    ' Display Contenders Table

                    WashingtonImage2.ImageUrl = "~/MLB ICONS/Washington Label.png"
                    WashingtonIcon2.ImageUrl = "~/MLB ICONS/Washington.png"
                    WashingtonImage2.BackColor = TeamBackColor

                    MiamiImage2.ImageUrl = "~/MLB ICONS/Miami Label.png"
                    MiamiIcon2.ImageUrl = "~/MLB ICONS/Miami.png"
                    MiamiImage2.BackColor = TeamBackColor

                    ColoradoImage2.ImageUrl = "~/MLB ICONS/Colorado Label.png"
                    ColoradoIcon2.ImageUrl = "~/MLB ICONS/Colorado.png"
                    ColoradoImage2.BackColor = TeamBackColor

                    ArizonaImage2.ImageUrl = "~/MLB ICONS/Arizona Label.png"
                    ArizonaIcon2.ImageUrl = "~/MLB ICONS/Arizona.png"
                    ArizonaImage2.BackColor = TeamBackColor

                    SanFranciscoImage2.ImageUrl = "~/MLB ICONS/San Francisco Label.png"
                    SanFranciscoIcon2.ImageUrl = "~/MLB ICONS/San Francisco.png"
                    SanFranciscoImage2.BackColor = TeamBackColor

                    SanDiegoImage2.ImageUrl = "~/MLB ICONS/San Diego Label.png"
                    SanDiegoIcon2.ImageUrl = "~/MLB ICONS/San Diego.png"
                    SanDiegoImage2.BackColor = TeamBackColor

                    PittsburgImage2.ImageUrl = "~/MLB ICONS/Pittsburg Label.png"
                    PittsburgIcon2.ImageUrl = "~/MLB ICONS/Pittsburg.png"
                    PittsburgImage2.BackColor = TeamBackColor

                    CincinnatiImage2.ImageUrl = "~/MLB ICONS/Cincinnati Label.png"
                    CincinnatiIcon2.ImageUrl = "~/MLB ICONS/Cincinnati.png"
                    CincinnatiImage2.BackColor = TeamBackColor

                    TorontoImage2.ImageUrl = "~/MLB ICONS/Toronto Label.png"
                    TorontoIcon2.ImageUrl = "~/MLB ICONS/Toronto.png"
                    TorontoImage2.BackColor = TeamBackColor

                    NYYankeesImage2.ImageUrl = "~/MLB ICONS/NY Yankees Label.png"
                    NYYankeesIcon2.ImageUrl = "~/MLB ICONS/NY Yankees.png"
                    NYYankeesImage2.BackColor = TeamBackColor

                    BostonImage2.ImageUrl = "~/MLB ICONS/Boston Label.png"
                    BostonIcon2.ImageUrl = "~/MLB ICONS/Boston.png"
                    BostonImage2.BackColor = TeamBackColor

                    TampaBayImage2.ImageUrl = "~/MLB ICONS/Tampa Bay Label.png"
                    TampaBayIcon2.ImageUrl = "~/MLB ICONS/Tampa Bay.png"
                    TampaBayImage2.BackColor = TeamBackColor

                    AtlantaImage2.ImageUrl = "~/MLB ICONS/Atlanta Label.png"
                    AtlantaIcon2.ImageUrl = "~/MLB ICONS/Atlanta.png"
                    AtlantaImage2.BackColor = TeamBackColor

                    PhiladelphiaImage2.ImageUrl = "~/MLB ICONS/Philadelphia Label.png"
                    PhiladelphiaIcon2.ImageUrl = "~/MLB ICONS/Philadelphia.png"
                    PhiladelphiaImage2.BackColor = TeamBackColor

                    ChicagoWhiteSoxImage2.ImageUrl = "~/MLB ICONS/Chicago White Sox Label.png"
                    ChicagoWhiteSoxIcon2.ImageUrl = "~/MLB ICONS/Chicago White Sox.png"
                    ChicagoWhiteSoxImage2.BackColor = TeamBackColor

                    DetroitImage2.ImageUrl = "~/MLB ICONS/Detroit Label.png"
                    DetroitIcon2.ImageUrl = "~/MLB ICONS/Detroit.png"
                    DetroitImage2.BackColor = TeamBackColor

                    KansasCityImage2.ImageUrl = "~/MLB ICONS/Kansas City Label.png"
                    KansasCityIcon2.ImageUrl = "~/MLB ICONS/Kansas City.png"
                    KansasCityImage2.BackColor = TeamBackColor

                    ClevelandImage2.ImageUrl = "~/MLB ICONS/Cleveland Label.png"
                    ClevelandIcon2.ImageUrl = "~/MLB ICONS/Cleveland.png"
                    ClevelandImage2.BackColor = TeamBackColor

                    MilwaukeeImage2.ImageUrl = "~/MLB ICONS/Milwaukee Label.png"
                    MilwaukeeIcon2.ImageUrl = "~/MLB ICONS/Milwaukee.png"
                    MilwaukeeImage2.BackColor = TeamBackColor

                    LADodgersImage2.ImageUrl = "~/MLB ICONS/LA Dodgers Label.png"
                    LADodgersIcon2.ImageUrl = "~/MLB ICONS/LA Dodgers.png"
                    LADodgersImage2.BackColor = TeamBackColor

                    MinnesotaImage2.ImageUrl = "~/MLB ICONS/Minnesota Label.png"
                    MinnesotaIcon2.ImageUrl = "~/MLB ICONS/Minnesota.png"
                    MinnesotaImage2.BackColor = TeamBackColor

                    OaklandImage2.ImageUrl = "~/MLB ICONS/Oakland Label.png"
                    OaklandIcon2.ImageUrl = "~/MLB ICONS/Oakland.png"
                    OaklandImage2.BackColor = TeamBackColor

                    HoustonImage2.ImageUrl = "~/MLB ICONS/Houston Label.png"
                    HoustonIcon2.ImageUrl = "~/MLB ICONS/Houston.png"
                    HoustonImage2.BackColor = TeamBackColor

                    TexasImage2.ImageUrl = "~/MLB ICONS/Texas Label.png"
                    TexasIcon2.ImageUrl = "~/MLB ICONS/Texas.png"
                    TexasImage2.BackColor = TeamBackColor

                    STLouisImage2.ImageUrl = "~/MLB ICONS/St. Louis Label.png"
                    STLouisIcon2.ImageUrl = "~/MLB ICONS/St. Louis.png"
                    STLouisImage2.BackColor = TeamBackColor

                    ChicagoCubsImage2.ImageUrl = "~/MLB ICONS/Chicago Cubs Label.png"
                    ChicagoCubsIcon2.ImageUrl = "~/MLB ICONS/Chicago Cubs.png"
                    ChicagoCubsImage2.BackColor = TeamBackColor

                    LAAngelsImage2.ImageUrl = "~/MLB ICONS/LA Angels Label.png"
                    LAAngelsIcon2.ImageUrl = "~/MLB ICONS/LA Angels.png"
                    LAAngelsImage2.BackColor = TeamBackColor

                    SeattleImage2.ImageUrl = "~/MLB ICONS/Seattle Label.png"
                    SeattleIcon2.ImageUrl = "~/MLB ICONS/Seattle.png"
                    SeattleImage2.BackColor = TeamBackColor

                    NYMetsImage2.ImageUrl = "~/MLB ICONS/NY Mets Label.png"
                    NYMetsIcon2.ImageUrl = "~/MLB ICONS/NY Mets.png"
                    NYMetsImage2.BackColor = TeamBackColor

                    BaltimoreImage2.ImageUrl = "~/MLB ICONS/Baltimore Label.png"
                    BaltimoreIcon2.ImageUrl = "~/MLB ICONS/Baltimore.png"
                    BaltimoreImage2.BackColor = TeamBackColor


                    Dim dailyUserChoices As New List(Of UserChoices)

                    dailyUserChoices = (From user1 In _DbLoserPool.UserChoicesList
                                            Where user1.Contender = True And user1.DayId = thisDay
                                            Select user1).ToList()

                    For Each user1 In dailyUserChoices

                        Dim user2 = New UserResult
                        user2.ListId = user1.ListId
                        user2.UserID = user1.UserID
                        user2.UserName = user1.UserName
                        user2.DayId = thisDay

                        If user1.UserPick = "Washington" Then
                            user2.Washington = False
                        Else
                            user2.Washington = user1.Washington
                        End If

                        If user1.UserPick = "Miami" Then
                            user2.Miami = False
                        Else
                            user2.Miami = user1.Miami
                        End If

                        If user1.UserPick = "Colorado" Then
                            user2.Colorado = False
                        Else
                            user2.Colorado = user1.Colorado
                        End If

                        If user1.UserPick = "Arizona" Then
                            user2.Arizona = False
                        Else
                            user2.Arizona = user1.Arizona
                        End If

                        If user1.UserPick = "San Francisco" Then
                            user2.SanFrancisco = False
                        Else
                            user2.SanFrancisco = user1.SanFrancisco
                        End If

                        If user1.UserPick = "San Diego" Then
                            user2.SanDiego = False
                        Else
                            user2.SanDiego = user1.SanDiego
                        End If

                        If user1.UserPick = "Pittsburg" Then
                            user2.Pittsburg = False
                        Else
                            user2.Pittsburg = user1.Pittsburg
                        End If

                        If user1.UserPick = "Cincinnati" Then
                            user2.Cincinnati = False
                        Else
                            user2.Cincinnati = user1.Cincinnati
                        End If

                        If user1.UserPick = "Toronto" Then
                            user2.Toronto = False
                        Else
                            user2.Toronto = user1.Toronto
                        End If

                        If user1.UserPick = "NY Yankees" Then
                            user2.NYYankees = False
                        Else
                            user2.NYYankees = user1.NYYankees
                        End If

                        If user1.UserPick = "Boston" Then
                            user2.Boston = False
                        Else
                            user2.Boston = user1.Boston
                        End If

                        If user1.UserPick = "Tampa Bay" Then
                            user2.TampaBay = False
                        Else
                            user2.TampaBay = user1.TampaBay
                        End If

                        If user1.UserPick = "Atlanta" Then
                            user2.Atlanta = False
                        Else
                            user2.Atlanta = user1.Atlanta
                        End If

                        If user1.UserPick = "Philadelphia" Then
                            user2.Philadelphia = False
                        Else
                            user2.Philadelphia = user1.Philadelphia
                        End If

                        If user1.UserPick = "Chicago White Sox" Then
                            user2.ChicagoWhiteSox = False
                        Else
                            user2.ChicagoWhiteSox = user1.ChicagoWhiteSox
                        End If

                        If user1.UserPick = "Detroit" Then
                            user2.Detroit = False
                        Else
                            user2.Detroit = user1.Detroit
                        End If

                        If user1.UserPick = "Kansas City" Then
                            user2.KansasCity = False
                        Else
                            user2.KansasCity = user1.KansasCity
                        End If

                        If user1.UserPick = "Cleveland" Then
                            user2.Cleveland = False
                        Else
                            user2.Cleveland = user1.Cleveland
                        End If

                        If user1.UserPick = "Milwaukee" Then
                            user2.Milwaukee = False
                        Else
                            user2.Milwaukee = user1.Milwaukee
                        End If

                        If user1.UserPick = "LA Dodgers" Then
                            user2.LADodgers = False
                        Else
                            user2.LADodgers = user1.LADodgers
                        End If

                        If user1.UserPick = "Minnesota" Then
                            user2.Minnesota = False
                        Else
                            user2.Minnesota = user1.Minnesota
                        End If

                        If user1.UserPick = "Oakland" Then
                            user2.Oakland = False
                        Else
                            user2.Oakland = user1.Oakland
                        End If

                        If user1.UserPick = "Houston" Then
                            user2.Houston = False
                        Else
                            user2.Houston = user1.Houston
                        End If

                        If user1.UserPick = "Texas" Then
                            user2.Texas = False
                        Else
                            user2.Texas = user1.Texas
                        End If

                        If user1.UserPick = "St. Louis" Then
                            user2.STLouis = False
                        Else
                            user2.STLouis = user1.STLouis
                        End If

                        If user1.UserPick = "Chicago Cubs" Then
                            user2.ChicagoCubs = False
                        Else
                            user2.ChicagoCubs = user1.ChicagoCubs
                        End If

                        If user1.UserPick = "LA Angels" Then
                            user2.LAAngels = False
                        Else
                            user2.LAAngels = user1.LAAngels
                        End If

                        If user1.UserPick = "Seattle" Then
                            user2.Seattle = False
                        Else
                            user2.Seattle = user1.Seattle
                        End If

                        If user1.UserPick = "NY Mets" Then
                            user2.NYMets = False
                        Else
                            user2.NYMets = user1.NYMets
                        End If

                        If user1.UserPick = "Baltimore" Then
                            user2.Baltimore = False
                        Else
                            user2.Baltimore = user1.Baltimore
                        End If

                        Dim dRow As New TableRow

                        Dim dCell1 As New TableCell
                        dCell1.Text = user2.UserName
                        dCell1.ForeColor = UserForeColor
                        dCell1.BackColor = UserBackColor
                        dCell1.CssClass = "UserName1"
                        dRow.Cells.Add(dCell1)

                        Dim dCell2 As New TableCell
                        If user2.Washington = True Then
                            dCell2.Text = "A"
                            dCell2.ForeColor = AvailableForeColor
                            dCell2.BackColor = AvailableBackColor
                        Else
                            dCell2.Text = "na"
                            dCell2.ForeColor = NotAvailableForeColor
                            dCell2.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell2)

                        Dim dCell3 As New TableCell
                        If user2.Miami = True Then
                            dCell3.Text = "A"
                            dCell3.ForeColor = AvailableForeColor
                            dCell3.BackColor = AvailableBackColor

                        Else
                            dCell3.Text = "na"
                            dCell3.ForeColor = NotAvailableForeColor
                            dCell3.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell3)

                        Dim dCell4 As New TableCell
                        If user2.Colorado = True Then
                            dCell4.Text = "A"
                            dCell4.ForeColor = AvailableForeColor
                            dCell4.BackColor = AvailableBackColor

                        Else
                            dCell4.Text = "na"
                            dCell4.ForeColor = NotAvailableForeColor
                            dCell4.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell4)

                        Dim dCell5 As New TableCell
                        If user2.Arizona = True Then
                            dCell5.Text = "A"
                            dCell5.ForeColor = AvailableForeColor
                            dCell5.BackColor = AvailableBackColor

                        Else
                            dCell5.Text = "na"
                            dCell5.ForeColor = NotAvailableForeColor
                            dCell5.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell5)

                        Dim dCell6 As New TableCell
                        If user2.SanFrancisco = True Then
                            dCell6.Text = "A"
                            dCell6.ForeColor = AvailableForeColor
                            dCell6.BackColor = AvailableBackColor

                        Else
                            dCell6.Text = "na"
                            dCell6.ForeColor = NotAvailableForeColor
                            dCell6.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell6)

                        Dim dCell7 As New TableCell
                        If user2.SanDiego = True Then
                            dCell7.Text = "A"
                            dCell7.ForeColor = AvailableForeColor
                            dCell7.BackColor = AvailableBackColor

                        Else
                            dCell7.Text = "na"
                            dCell7.ForeColor = NotAvailableForeColor
                            dCell7.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell7)

                        Dim dCell8 As New TableCell
                        If user2.Pittsburg = True Then
                            dCell8.Text = "A"
                            dCell8.ForeColor = AvailableForeColor
                            dCell8.BackColor = AvailableBackColor

                        Else
                            dCell8.Text = "na"
                            dCell8.ForeColor = NotAvailableForeColor
                            dCell8.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell8)

                        Dim dCell9 As New TableCell
                        If user2.Cincinnati = True Then
                            dCell9.Text = "A"
                            dCell9.ForeColor = AvailableForeColor
                            dCell9.BackColor = AvailableBackColor

                        Else
                            dCell9.Text = "na"
                            dCell9.ForeColor = NotAvailableForeColor
                            dCell9.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell9)

                        Dim dCell10 As New TableCell
                        If user2.Toronto = True Then
                            dCell10.Text = "A"
                            dCell10.ForeColor = AvailableForeColor
                            dCell10.BackColor = AvailableBackColor

                        Else
                            dCell10.Text = "na"
                            dCell10.ForeColor = NotAvailableForeColor
                            dCell10.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell10)

                        Dim dCell11 As New TableCell
                        If user2.NYYankees = True Then
                            dCell11.Text = "A"
                            dCell11.ForeColor = AvailableForeColor
                            dCell11.BackColor = AvailableBackColor

                        Else
                            dCell11.Text = "na"
                            dCell11.ForeColor = NotAvailableForeColor
                            dCell11.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell11)

                        Dim dCell12 As New TableCell
                        If user2.Boston = True Then
                            dCell12.Text = "A"
                            dCell12.ForeColor = AvailableForeColor
                            dCell12.BackColor = AvailableBackColor

                        Else
                            dCell12.Text = "na"
                            dCell12.ForeColor = NotAvailableForeColor
                            dCell12.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell12)

                        Dim dCell13 As New TableCell
                        If user2.TampaBay = True Then
                            dCell13.Text = "A"
                            dCell13.ForeColor = AvailableForeColor
                            dCell13.BackColor = AvailableBackColor

                        Else
                            dCell13.Text = "na"
                            dCell13.ForeColor = NotAvailableForeColor
                            dCell13.BackColor = NotAvailableBackColor
                        End If
                        dRow.Cells.Add(dCell13)

                        Dim dCell14 As New TableCell
                        If user2.Atlanta = True Then
                            dCell14.Text = "A"
                            dCell14.ForeColor = AvailableForeColor
                            dCell14.BackColor = AvailableBackColor

                        Else
                            dCell14.Text = "na"
                            dCell14.ForeColor = NotAvailableForeColor
                            dCell14.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell14)

                        Dim dCell15 As New TableCell
                        If user2.Philadelphia = True Then
                            dCell15.Text = "A"
                            dCell15.ForeColor = AvailableForeColor
                            dCell15.BackColor = AvailableBackColor
                        Else
                            dCell15.Text = "na"
                            dCell15.ForeColor = NotAvailableForeColor
                            dCell15.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell15)

                        Dim dCell16 As New TableCell
                        If user2.ChicagoWhiteSox = True Then
                            dCell16.Text = "A"
                            dCell16.ForeColor = AvailableForeColor
                            dCell16.BackColor = AvailableBackColor

                        Else
                            dCell16.Text = "na"
                            dCell16.ForeColor = NotAvailableForeColor
                            dCell16.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell16)

                        Dim dCell17 As New TableCell
                        If user2.Detroit = True Then
                            dCell17.Text = "A"
                            dCell17.ForeColor = AvailableForeColor
                            dCell17.BackColor = AvailableBackColor

                        Else
                            dCell17.Text = "na"
                            dCell17.ForeColor = NotAvailableForeColor
                            dCell17.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell17)

                        Dim dCell18 As New TableCell
                        If user2.KansasCity = True Then
                            dCell18.Text = "A"
                            dCell18.ForeColor = AvailableForeColor
                            dCell18.BackColor = AvailableBackColor

                        Else
                            dCell18.Text = "na"
                            dCell18.ForeColor = NotAvailableForeColor
                            dCell18.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell18)

                        Dim dCell19 As New TableCell
                        If user2.Cleveland = True Then
                            dCell19.Text = "A"
                            dCell19.ForeColor = AvailableForeColor
                            dCell19.BackColor = AvailableBackColor

                        Else
                            dCell19.Text = "na"
                            dCell19.ForeColor = NotAvailableForeColor
                            dCell19.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell19)

                        Dim dCell20 As New TableCell
                        If user2.Milwaukee = True Then
                            dCell20.Text = "A"
                            dCell20.ForeColor = AvailableForeColor
                            dCell20.BackColor = AvailableBackColor

                        Else
                            dCell20.Text = "na"
                            dCell20.ForeColor = NotAvailableForeColor
                            dCell20.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell20)

                        Dim dCell21 As New TableCell
                        If user2.LADodgers = True Then
                            dCell21.Text = "A"
                            dCell21.ForeColor = AvailableForeColor
                            dCell21.BackColor = AvailableBackColor

                        Else
                            dCell21.Text = "na"
                            dCell21.ForeColor = NotAvailableForeColor
                            dCell21.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell21)

                        Dim dCell22 As New TableCell
                        If user2.Minnesota = True Then
                            dCell22.Text = "A"
                            dCell22.ForeColor = AvailableForeColor
                            dCell22.BackColor = AvailableBackColor

                        Else
                            dCell22.Text = "na"
                            dCell22.ForeColor = NotAvailableForeColor
                            dCell22.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell22)

                        Dim dCell23 As New TableCell
                        If user2.Oakland = True Then
                            dCell23.Text = "A"
                            dCell23.ForeColor = AvailableForeColor
                            dCell23.BackColor = AvailableBackColor

                        Else
                            dCell23.Text = "na"
                            dCell23.ForeColor = NotAvailableForeColor
                            dCell23.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell23)

                        Dim dCell24 As New TableCell
                        If user2.Houston = True Then
                            dCell24.Text = "A"
                            dCell24.ForeColor = AvailableForeColor
                            dCell24.BackColor = AvailableBackColor

                        Else
                            dCell24.Text = "na"
                            dCell24.ForeColor = NotAvailableForeColor
                            dCell24.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell24)

                        Dim dCell25 As New TableCell
                        If user2.Texas = True Then
                            dCell25.Text = "A"
                            dCell25.ForeColor = AvailableForeColor
                            dCell25.BackColor = AvailableBackColor

                        Else
                            dCell25.Text = "na"
                            dCell25.ForeColor = NotAvailableForeColor
                            dCell25.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell25)

                        Dim dCell26 As New TableCell
                        If user2.STLouis = True Then
                            dCell26.Text = "A"
                            dCell26.ForeColor = AvailableForeColor
                            dCell26.BackColor = AvailableBackColor

                        Else
                            dCell26.Text = "na"
                            dCell26.ForeColor = NotAvailableForeColor
                            dCell26.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell26)

                        Dim dCell27 As New TableCell
                        If user2.ChicagoCubs = True Then
                            dCell27.Text = "A"
                            dCell27.ForeColor = AvailableForeColor
                            dCell27.BackColor = AvailableBackColor

                        Else
                            dCell27.Text = "na"
                            dCell27.ForeColor = NotAvailableForeColor
                            dCell27.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell27)

                        Dim dCell28 As New TableCell
                        If user2.LAAngels = True Then
                            dCell28.Text = "A"
                            dCell28.ForeColor = AvailableForeColor
                            dCell28.BackColor = AvailableBackColor

                        Else
                            dCell28.Text = "na"
                            dCell28.ForeColor = NotAvailableForeColor
                            dCell28.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell28)

                        Dim dCell29 As New TableCell
                        If user2.Seattle = True Then
                            dCell29.Text = "A"
                            dCell29.ForeColor = AvailableForeColor
                            dCell29.BackColor = AvailableBackColor

                        Else
                            dCell29.Text = "na"
                            dCell29.ForeColor = NotAvailableForeColor
                            dCell29.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell29)

                        Dim dCell30 As New TableCell
                        If user2.NYMets = True Then
                            dCell30.Text = "A"
                            dCell30.ForeColor = AvailableForeColor
                            dCell30.BackColor = AvailableBackColor

                        Else
                            dCell30.Text = "na"
                            dCell30.ForeColor = NotAvailableForeColor
                            dCell30.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell30)

                        Dim dCell31 As New TableCell
                        If user2.Baltimore = True Then
                            dCell31.Text = "A"
                            dCell31.ForeColor = AvailableForeColor
                            dCell31.BackColor = AvailableBackColor

                        Else
                            dCell31.Text = "na"
                            dCell31.ForeColor = NotAvailableForeColor
                            dCell31.BackColor = NotAvailableBackColor

                        End If
                        dRow.Cells.Add(dCell31)

                        Contenders4.Rows.Add(dRow)

                    Next

                    Contenders4.DataBind()


                    Dim queryLosers = (From loser1 In _DbLoserPool.LoserList
                                       Order By loser1.DayId Descending).ToList

                    Dim queryDays = (From timePeriod1 In _DbLoserPool.ScheduleTimePeriods
                                       Where Not timePeriod1.startDayDate Is Nothing
                                       Order By timePeriod1.dayID Descending).ToList

                    For Each day1 In queryDays

                        Dim queryDayLosers = (From loser1 In _DbLoserPool.LoserList
                                              Where loser1.DayId = day1.dayID).ToList

                        For Each loser1 In queryDayLosers

                            If loser1.LosingPick <> "Not Made" Then
                                Dim loser2 = New Loser
                                loser2.UserName = loser1.UserName
                                loser2.DayId = loser1.DayId
                                loser2.LosingPick = loser1.LosingPick
                                LoserCollectionSorted.Add(loser2.UserName, loser2)
                            End If
                        Next
                        For Each loser1 In queryDayLosers
                            If loser1.LosingPick = "Not Made" Then
                                Dim loser2 = New Loser
                                loser2.UserName = loser1.UserName
                                loser2.DayId = loser1.DayId
                                loser2.LosingPick = loser1.LosingPick
                                LoserCollectionSorted.Add(loser2.UserName, loser2)
                            End If
                        Next


                    Next


                    For Each loser1 In LoserCollectionSorted

                        Dim dRow As New TableRow

                        Dim dCell1 As New TableCell
                        dCell1.Text = loser1.Value.UserName
                        dCell1.ForeColor = Drawing.Color.Red
                        dCell1.BackColor = Drawing.Color.LightSalmon
                        dRow.Cells.Add(dCell1)

                        Dim dCell2 As New TableCell
                        dCell2.Text = loser1.Value.DayId
                        dCell2.ForeColor = Drawing.Color.Red
                        dCell2.BackColor = Drawing.Color.LightSalmon
                        dRow.Cells.Add(dCell2)

                        Dim dCell3 As New TableCell
                        dCell3.Text = loser1.Value.LosingPick
                        dCell3.ForeColor = Drawing.Color.Red
                        dCell3.BackColor = Drawing.Color.LightSalmon
                        dRow.Cells.Add(dCell3)

                        LoserTable1.Rows.Add(dRow)
                    Next

                    LoserTable1.DataBind()

                    ViewState("dayNumber") = thisDay



                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Shared Function SetHomeTeamAvailabilityState(gameUpdate1 As GameUpdate, team As String, teamAvailability As Boolean, user1 As UserChoices) As GameUpdate
        If gameUpdate1.HomeTeam = team Then
            If user1.UserPick = team Then
                gameUpdate1.HomeTeamAvailability.Add(user1.UserName, "L")
            ElseIf teamAvailability = True Then
                gameUpdate1.HomeTeamAvailability.Add(user1.UserName, "A")
            Else
                gameUpdate1.HomeTeamAvailability.Add(user1.UserName, "NA")
            End If
        End If

        Return gameUpdate1
    End Function

    Private Shared Function SetAwayTeamAvailabilityState(gameUpdate1 As GameUpdate, team As String, teamAvailability As Boolean, user1 As UserChoices) As GameUpdate
        If gameUpdate1.AwayTeam = team Then
            If user1.UserPick = team Then
                gameUpdate1.AwayTeamAvailability.Add(user1.UserName, "L")
            ElseIf teamAvailability = True Then
                gameUpdate1.AwayTeamAvailability.Add(user1.UserName, "A")
            Else
                gameUpdate1.AwayTeamAvailability.Add(user1.UserName, "NA")
            End If
        End If

        Return gameUpdate1
    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Response.Redirect("~/LosersPool/Default.aspx")
    End Sub


End Class