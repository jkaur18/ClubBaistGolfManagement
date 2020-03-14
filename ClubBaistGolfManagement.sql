Create database ClubBaistSystem


Drop table if exists TeeTime

Create Table TeeTime
(
	Date Date,
	TimeSlot Time Not Null,
	Player1Number		nvarchar(450) Foreign Key References AspNetUsers(Id),
	Player2Number		nvarchar(450) Foreign Key References AspNetUsers(Id),
	Player3Number		nvarchar(450) Foreign Key References AspNetUsers(Id),
	Player4Number		nvarchar(450) Foreign Key References AspNetUsers(Id),
	BookerNumber		nvarchar(450) Foreign Key References AspNetUsers(Id),
	Player1CheckedIn	bit,
	Player2CheckedIn	bit,
	Player3CheckedIn	bit,
	Player4CheckedIn	bit,	
	Constraint PK_Date_TimeSlot Primary key clustered (Date, TimeSlot)
)

Drop table if exists StandingTeeTimeRequest

Create Table StandingTeeTimeRequest
(
	DayofWeek			Varchar(9),
	RequestedTeeTime	Time Not Null,
	RequestedStartDate	Date Not Null,
	RequestedEndDate	Date Not Null,
	Shareholder1Number	nvarchar(450) Foreign Key References AspNetUsers(Id),
	Shareholder2Number	nvarchar(450) Foreign Key References AspNetUsers(Id),
	Shareholder3Number	nvarchar(450) Foreign Key References AspNetUsers(Id),
	Shareholder4Number	nvarchar(450) Foreign Key References AspNetUsers(Id),
	BookerNumber		nvarchar(450) Foreign Key References AspNetUsers(Id),
	Constraint PK_Day_TeeTime_Date Primary key clustered (DayofWeek, RequestedTeeTime, RequestedStartDate, RequestedEndDate)
)

Drop table if exists MembershipApplication

CREATE TABLE MembershipApplication
(
	MembershipApplicationId INT IDENTITY(1,1) PRIMARY KEY,
	LastName				NVARCHAR(25),
	FirstName				NVARCHAR(25),
	[Address]				NVARCHAR(50),
	PostalCode				NVARCHAR(6),
	City					NVARCHAR(25),
	DateOfBirth				DATE,
	Shareholder1			NVARCHAR(25),
	Shareholder2			NVARCHAR(25),
	MembershipType			NVARCHAR(15),
	Occupation				NVARCHAR(15),
	CompanyName				NVARCHAR(40),
	CompanyAddress			NVARCHAR(50),
	CompanyPostalCode		NVARCHAR(6),
	CompanyCity				NVARCHAR(25),
	CompanyPhone			NVARCHAR(10),
	Email					NVARCHAR(50),
	Phone					NVARCHAR(10), 
	AlternatePhone			NVARCHAR(10) ,
	ApplicationStatus		NVARCHAR(10) CONSTRAINT DF_MemberApplication_Status DEFAULT 'On Hold'
)

Drop Table if Exists AccountEntry

Create Table AccountEntry
(
	MemberID			Nvarchar(450),
	WhenCharged			DateTime,
	WhenMade			DateTime,
	Amount				Money,
	PaymentDescription nvarchar(150),	
	Constraint PK_AccountEntry Primary Key nonclustered (MemberID,WhenCharged),
	Constraint FK_AccountEntry Foreign Key (MemberID) references AspNetUsers(Id)
)

Drop Table if exists GolfRounds

Create Table GolfRounds
(
	PlayerNumber	Nvarchar(450),
	CourseName		Nvarchar(25),
	[Date]			Date,
	Hole			Int,
	Score			Int,
	Rating			Decimal,
	Slope			Decimal,
	Constraint PK_GolfRounds Primary Key nonclustered (PlayerNumber, CourseName, [Date], Hole),
	Constraint FK_GolfRounds Foreign Key (PlayerNumber) References AspNetUsers(Id)
)



go
Create or Alter procedure AddTeeTime(@Date Date, @TimeSlot Time, @player1 nvarchar(450), @player2 nvarchar(450)=NULL, @player3 nvarchar(450)=NULL, @player4 nvarchar(450)=NULL, @booker nvarchar(50))
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	IF @Date IS NULL
		RAISERROR ('Add TeeTime - Required parameter: @Date',16,1)	
	Else IF @TimeSlot IS NULL
		RAISERROR ('Add TeeTime - Required parameter: @TimeSlot',16,1)
	Else IF @player1 IS NULL
		RAISERROR ('Add TeeTime - Required parameter: @player1',16,1)
	
	Else
	Begin
		UPDATE TeeTime
			Set Player1Number = @player1,
				Player2Number = @player2,
				Player3Number = @player3,
				Player4Number = @player4,
				BookerNumber = @booker
			WHERE [Date] = @Date AND TimeSlot = @TimeSlot

		If @@ERROR = 0 
				Set @ReturnCode = 0
			Else
		RaisError('Add TeeTime - Update Error from TeeTime database',16,1)		
		Return @ReturnCode
	End


	go
Create or Alter procedure CancelTeeTime(@Date Date, @TimeSlot Time)
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	IF @Date IS NULL
		RAISERROR ('Cancel TeeTime - Required parameter: @Date',16,1)	
	Else IF @TimeSlot IS NULL
		RAISERROR ('Cancel TeeTime - Required parameter: @TimeSlot',16,1)	
	Else
	Begin
		UPDATE TeeTime
			Set Player1Number = null,
				Player2Number = null,
				Player3Number = null,
				Player4Number = null,
				BookerNumber = null
			WHERE [Date] = @Date AND TimeSlot = @TimeSlot

		If @@ERROR = 0 
				Set @ReturnCode = 0
			Else
		RaisError('Cancel tee time - Update error from tee time database',16,1)
		Return @ReturnCode
	End
	

go
Create or alter Procedure GenerateDailyTeeSheet(@AddDay int)
AS
	Declare @starttime DateTime, @endtime DateTime, @counter int = 0
		set @starttime = '7:00am' 
		set @endtime = '7:01pm'	
	
	while (@starttime<@endtime)
	Begin		
		insert into TeeTime 
		values 
		(
			(Select Convert(varchar(10),GETDATE()+@AddDay,111)),
			@starttime, null, null, null, null, null, 0, 0, 0, 0
		)

		SET @counter = @counter + 1
		 
		if @counter % 2 = 0
			set @starttime = DATEADD(MINUTE,8,@starttime)
		else
			set @starttime = DATEADD(MINUTE,7,@starttime)
	end
	 set @counter = 0
	 while(@counter)<(Select count(Shareholder1Number) from StandingTeeTimeRequest 
		Where Shareholder1Number is not null and Getdate() between 
	RequestedStartDate and RequestedEndDate and DayofWeek = Format (GETDATE(),'dddd'))
	 begin
		set @counter = @counter +1;
		with GetUser as 
		(Select *,(ROW_NUMBER() Over(order by Shareholder1Number)) As Rownumber 
		from StandingTeeTimeRequest 
			where Shareholder1Number is not null and GETDATE() between RequestedStartDate and RequestedEndDate and DayofWeek = format(GETDATE(),'dddd'))
			
		Update TeeTime
	set Player1Number = (select Player1Number from GetUser where Rownumber = @counter),
	Player2Number = (select Player2Number from GetUser where Rownumber = @counter),
	Player3Number = (select Player3Number from GetUser where Rownumber = @counter),
	Player4Number = (select Player2Number from GetUser where Rownumber = @counter)
		where
		Date = (Select convert(Varchar(10),getdate(),111))
	and TimeSlot = (Select RequestedTeeTime from GetUser where Rownumber = @counter )

End

	
	
	exec GenerateDailyTeeSheet 0

	exec GenerateDailyTeeSheet 1

	exec GenerateDailyTeeSheet 2

	exec GenerateDailyTeeSheet 3

	exec GenerateDailyTeeSheet 4

	exec GenerateDailyTeeSheet 5

	exec GenerateDailyTeeSheet 6

	exec GenerateDailyTeeSheet 7

	

go
	create or alter procedure GenerateStandingTeeTime (@DayofWeek varchar(9))
	as
		Declare @starttime DateTime, @endtime DateTime, @counter int = 0
	
		set @starttime = '7:00am' 
		set @endtime = '7:01pm'	
	
	while (@starttime<@endtime)
	Begin		
		insert into StandingTeeTimeRequest 
		values 
		( 
			@DayofWeek, @starttime, '2020-02-01', '2020-09-30',
			null, null, null, null, null
		)

		SET @counter = @counter + 1
		 
		if @counter % 2 = 0
			set @starttime = DATEADD(MINUTE,8,@starttime)
		else
			set @starttime = DATEADD(MINUTE,7,@starttime)
	end
	go
	
	
	
	GenerateStandingTeeTime 'Monday'
	go
	GenerateStandingTeeTime 'Tuesday'
	go
	GenerateStandingTeeTime 'Wednesday'
	go
	GenerateStandingTeeTime 'Thursday'
	go
	GenerateStandingTeeTime 'Friday'
	go
	GenerateStandingTeeTime 'Saturday'
	go
	GenerateStandingTeeTime 'Sunday'


go
Create or Alter Procedure GetAvailableStandingTeeTime(@dayofweek varchar(9))
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	IF @dayofweek is Null
	RAISERROR ('GetAvailableStandingTeeTime-Required parameter: @DayofWeek',16,1)
	Else
	Begin
		Select
			RequestedTeeTime,
			DayofWeek,
			Shareholder1Number,
			Shareholder2Number,
			Shareholder3Number,
			Shareholder4Number
			from StandingTeeTimeRequest	
			Where DayofWeek = @dayofweek and Shareholder1Number is null

		If @@ERROR = 0
				Set @ReturnCode = 0
			Else
	RaisError('GetAvailableStandingTeeTime - Select Error from StandingTeeTime Database',16,1)
		
		Return @ReturnCode	
	End

go
Create or Alter Procedure GetReservedStandingTeeTime(@dayofweek varchar(9), @time time, @startDate date, @endDate date)
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	IF @dayofweek is Null
	RAISERROR ('GetReservedStandingTeeTime -Required parameter: @DayofWeek',16,1)
	Else
	Begin
		Select
			RequestedTeeTime,
			DayofWeek,
			Shareholder1Number,
			Shareholder2Number,
			Shareholder3Number,
			Shareholder4Number
			from StandingTeeTimeRequest	
			Where DayofWeek = @dayofweek 
			and RequestedTeeTime = @time
			and RequestedStartDate = @startDate
			and RequestedEndDate = @endDate

		If @@ERROR = 0
				Set @ReturnCode = 0
			Else
	RaisError('GetReservedStandingTeeTime - Select Error from StandingTeeTime Database',16,1)
		
		Return @ReturnCode	
	End

go

Create or Alter Procedure AddStandingTeeTime(@reqTime Time, @dayofweek varchar(9), @reqStartDate Date, @reqEndDate Date, @shareholder1 nvarchar(450),
	@Shareholder2 nvarchar(450), @shareholder3 nvarchar(450), @shareholder4 nvarchar(450), @booker nvarchar(450))
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	IF @dayofweek is Null
	RAISERROR ('Add Standing Tee Time Request-Required parameter: @dayofweek',16,1)
	Else
	Begin
		Update StandingTeeTimeRequest
		set RequestedStartDate = @reqStartDate,
			RequestedEndDate = @reqEndDate,
			Shareholder1Number = @shareholder1,
			Shareholder2Number = @shareholder2,
			Shareholder3Number = @shareholder3,
			Shareholder4Number = @shareholder4,
			BookerNumber = @booker
			Where DayofWeek = @dayofweek and RequestedTeeTime = @reqTime

		If @@ERROR = 0
				Set @ReturnCode = 0
			Else
	RaisError('Add Standing Tee Time Request- Update Error from StandingTeeTime Database',16,1)
		Return @ReturnCode	
	End

	
	go 
Create or Alter Procedure GetUser(@userid nvarchar(450))
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	IF @userid IS NULL
		RAISERROR ('Lookup User - Required parameter: @userid',16,1)
	Else
	Begin
		 Select AspNetUsers.Id, AspNetUsers.FullName,AspNetRoles.Name as Role
  from AspNetUsers Inner join AspNetUserRoles
  On AspNetUsers.Id = AspNetUserRoles.UserId
  inner join AspNetRoles
  on AspNetUserRoles.RoleId = AspNetRoles.Id
  Where AspNetUsers.Id = @userid

		If @@ERROR = 0
				Set @ReturnCode = 0
			Else
			RaisError('Lookup User - Select Error from AspNetUsers database',16,1)
		
		Return @ReturnCode
	End	

	go 

	select * from ASPNetUsers
		go
Create or Alter Procedure GetUserByEmail(@email nvarchar(450))
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	Begin
		 Select AspNetUsers.Id, AspNetUsers.FullName,AspNetRoles.Name as Role
  from AspNetUsers Inner join AspNetUserRoles
  On AspNetUsers.Id = AspNetUserRoles.UserId
  inner join AspNetRoles
  on AspNetUserRoles.RoleId = AspNetRoles.Id
  Where AspNetUsers.UserName = @email    

		If @@ERROR = 0
				Set @ReturnCode = 0
			Else
			RaisError('Lookup User by email - Select Error from AspNetUsers database',16,1)
		
		Return @ReturnCode
	End


go
	Create or Alter Procedure FindTeeTime(@date Date, @time Time)
As
Declare @ReturnCode int
	Set @ReturnCode = 1
	
	Begin
		Select * from TeeTime
		where Date = @date and TimeSlot = @time

		If @@ERROR = 0
				Set @ReturnCode = 0
			Else
			RaisError('Lookup TeeTime - Select Error from TeeTime database',16,1)
		
		Return @ReturnCode
	End


	go
		Create or Alter Procedure CheckInPlayer(@date Date, @time Time,@player1status bit=0, @player2status bit = 0, @player3status bit =0, @player4status bit = 0)
As
Declare @ReturnCode int
	Set @ReturnCode = 1
	
	Begin
		Update TeeTime
		set Player1CheckedIn = @player1status,
			Player2CheckedIn = @player2status,
			Player3CheckedIn = @player3status,
			Player4CheckedIn = @player4status
		where Date = @date and TimeSlot = @time

		If @@ERROR = 0
				Set @ReturnCode = 0
			Else
			RaisError('Check In Player - Update Error from TeeTime database',16,1)
		
		Return @ReturnCode
	End

	
go
	Create or Alter Procedure GetUserIdFromEmail(@email NVARCHAR(256))
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	SELECT Id FROM AspNetUsers
	WHERE UserName = @email

			If @@ERROR = 0
				Set @ReturnCode = 0
			Else
		
			RaisError('GetUserIdFromEmail - Select Error from AspNetUsers Table',16,1)
		
		Return @ReturnCode

		go
	Create or Alter Procedure GetUserIdByName(@username NVARCHAR(256))
As
Declare @ReturnCode int
	Set @ReturnCode = 1

	SELECT Id FROM AspNetUsers
	WHERE FullName = @username

			If @@ERROR = 0
				Set @ReturnCode = 0
			Else
		
			RaisError('GetUserByName - Select Error from AspNetUsers Table',16,1)
		
		Return @ReturnCode
	
go
Create or Alter procedure RecordMembershipApplication
	(
		@lastname nvarchar(25), 
		@firstname nvarchar(25), 
		@address nvarchar(50), 
		@postalcode nvarchar(6),
		@city nvarchar(25),
		@dateofbirth Date,
		@email nvarchar(50),
		@phone nvarchar(10),
		@alternatephone nvarchar(10),
		@occupation nvarchar(15),
		@companyname nvarchar(40),
		@companyaddress nvarchar(50),
		@companypostalcode nvarchar(6),
		@companycity nvarchar(25),
		@companyphone nvarchar(10),
		@shareholder1 nvarchar(50),
		@shareholder2 nvarchar(50),
		@membershiptype nvarchar(15)
	)
As
Declare @ReturnCode int
	Set @ReturnCode = 1
	
	
	Begin
		Insert into MembershipApplication(LastName, FirstName, Address, PostalCode, City, DateOfBirth,Email, Phone, AlternatePhone,Occupation, CompanyName,CompanyAddress,CompanyPostalCode, CompanyCity,CompanyPhone, Shareholder1,Shareholder2, MembershipType)
		Values (@lastname, @firstname,@address, @postalcode, @city, @dateofbirth, @email,  @phone, @alternatephone, @occupation,@companyname, @companyaddress, @companypostalcode,@companyphone, @companycity,@shareholder1, @shareholder2, @membershiptype)
			

		If @@ERROR = 0 
				Set @ReturnCode = 0
			Else
		RaisError('Record Membership Application - Insert Error from MembershipApplication database',16,1)		
		Return 
	End

	go
Create or Alter procedure GetMembershipApplications
	
As
Declare @ReturnCode int
	Set @ReturnCode = 1	
	
	Begin
		Select MembershipApplicationId, LastName, FirstName, MembershipType, ApplicationStatus 
		from MembershipApplication
		where ApplicationStatus = 'On Hold' or ApplicationStatus ='Waitlisted' or ApplicationStatus ='Cancelled' or ApplicationStatus = 'Approved'
		If @@ERROR = 0 
				Set @ReturnCode = 0
			Else
		RaisError('Get Membership Applications - Select Error from MembershipApplication database',16,1)		
		Return 
	End

	go
Create or Alter procedure GetMembershipApplicationDetails(@applicationid int)
	
As
Declare @ReturnCode int
	Set @ReturnCode = 1	
	
	Begin
		Select * from MembershipApplication
		where MembershipApplicationId = @applicationid
		If @@ERROR = 0 
				Set @ReturnCode = 0
			Else
		RaisError('Get Membership Application Details- Select Error from MembershipApplication database',16,1)		
		Return 
	End


	go
	Create or Alter procedure WaitlistMembershipApplication(@applicationid int)
	
As
Declare @ReturnCode int
	Set @ReturnCode = 1	
	
	Begin
		Update MembershipApplication
		set ApplicationStatus = 'Waitlisted'
		where MembershipApplicationId = @applicationid
		If @@ERROR = 0 
				Set @ReturnCode = 0
			Else
		RaisError('Waitlist Membership Application - Update Error from MembershipApplication database',16,1)		
		Return 
	End


		go
	Create or Alter procedure CancelMembershipApplication(@applicationid int)
	
As
Declare @ReturnCode int
	Set @ReturnCode = 1	
	
	Begin
		Update MembershipApplication
		set ApplicationStatus = 'Cancelled'
		where MembershipApplicationId = @applicationid
		If @@ERROR = 0 
				Set @ReturnCode = 0
			Else
		RaisError('Cancel Membership Application - Update Error from MembershipApplication database',16,1)		
		Return 
	End

	go
	Create or Alter procedure ApproveMembershipApplication(@applicationid int)
	
As
Declare @ReturnCode int
	Set @ReturnCode = 1	
	
	Begin
		Update MembershipApplication
		set ApplicationStatus = 'Approved'
		where MembershipApplicationId = @applicationid
		If @@ERROR = 0 
				Set @ReturnCode = 0
			Else
		RaisError('Approve Membership Application - Update Error from MembershipApplication database',16,1)		
		Return 
	End

	go
	Create or Alter Procedure GetMemberAccount(@memberid nvarchar(450))
	as
	Declare @ReturnCode int
	Set @ReturnCode = 1	
	
	Begin
		Select MemberID, WhenCharged, WhenMade, Amount, PaymentDescription, (Select(Sum(Amount)) from AccountEntry Where MemberID = @memberid)as Balance
			from AccountEntry
			Where MemberID = @memberid
	If @@ERROR = 0 
				Set @ReturnCode = 0
			Else
		RaisError('Get Member Account - Select Error from Account Entry database',16,1)		
		Return 
	End

	go
	Create or Alter Procedure AddPlayerScore(@playernumber nvarchar(450), @coursename nvarchar(25), @date Date, @hole int, @score int, @rating decimal, @slope decimal )
	as
	Declare @ReturnCode int
	Set @ReturnCode = 1	
	
	Begin
		Insert into GolfRounds (PlayerNumber, CourseName, [Date], Hole, Score, Rating, Slope )
		values(@playernumber, @coursename, @date, @hole, @score, @rating, @slope)
	If @@ERROR = 0 
				Set @ReturnCode = 0
			Else
		RaisError('Add Player Scores - Insert Error from GolfRounds database',16,1)		
		Return 
	End

	
	go 
	Create or Alter Procedure CreateMemberAccount (@newid nvarchar(450), @username nvarchar(256),@normalizedusername nvarchar(256), @passwordhash nvarchar(max), @fullname nvarchar(max), @memberdescription nvarchar(max), @membershipapplicationid int)
	as
	Declare @ReturnCode int
	Set @ReturnCode = 1	
	Declare @annualfees int
	
	IF @memberdescription = 'Shareholder'
		SET @annualfees = 3500
	ELSE If @memberdescription = 'Associate'
		SET @annualfees = 5000

	 INSERT INTO AspNetUsers 
	 (	 Id, UserName, NormalizedUserName, Email, EmailConfirmed, NormalizedEmail, PasswordHash, FullName, PhoneNumberConfirmed, TwoFactorEnabled, 
		 LockoutEnabled,AccessFailedCount, SecurityStamp, ConcurrencyStamp )
	 VALUES 
	 (
		@newid, @userName, @normalizedUserName, @userName, 1 , @normalizedUserName,  @passwordHash, @fullName, 1, 0, 0, 0,
	  'IZJR3PV5XOBCOLPKDS7JIGHAHVFI7AH4', 
	  'd139d99f-4ac1-48ce-95c2-677fb68f87f6' 
	  )
	 
	 INSERT INTO AspNetUserRoles (UserId,RoleId)
	 VALUES
		(
			@newid, (SELECT Top 1 AspNetRoles.Id FROM AspNetUserRoles
			INNER JOIN AspNetRoles
			ON AspNetRoles.Id = AspNetUserRoles.RoleId
			Where Name = @memberdescription) 
		)

	UPDATE MembershipApplication
	SET	  ApplicationStatus = 'Approve'
	WHERE  MembershipApplicationId = @membershipapplicationid	

	INSERT INTO AccountEntry (MemberID, WhenCharged, WhenMade, Amount, PaymentDescription)
	VALUES (@newid, GETDATE(), GETDATE(), @annualfees, 'Yearly Membership Fees')
	 
	 IF @@ERROR = 0
			SET @ReturnCode = 0
	 ELSE 
		RAISERROR('CreateMemberAccount - INSERT error from AspNetUser Table',16,1)

	 Return @ReturnCode 


	 go
Create or alter Procedure ViewPlayerHandicap (@playernumber nvarchar(450))
AS 
	 DECLARE		@ReturnCode int
	 SET			@ReturnCode = 1

	 Select Top 20 Hole,Score, Rating, Slope from GolfRounds
	 Where PlayerNumber = @playernumber
		and Score ! = 0
		Order by [Date] Desc, Hole Desc

	IF @@ERROR = 0
			SET @ReturnCode = 0
	 ELSE 
		RAISERROR('ViewPlayerHandicap - Select error from AspNetUser Table',16,1)

	 Return @ReturnCode 



	