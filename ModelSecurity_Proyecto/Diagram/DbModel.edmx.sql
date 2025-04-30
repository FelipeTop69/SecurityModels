
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/01/2025 16:34:16
-- Generated from EDMX file: C:\Users\USUARIO\Documents\ADSO-2900177\tecnico\2025\Marzo\c#\ModelSecurityProyecto\Diagram\DbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [carnet];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_UserPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleModuleRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleFormPermissioSet] DROP CONSTRAINT [FK_RoleModuleRole];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRoleRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK_UserRoleRole];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRoleUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK_UserRoleUser];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleModulePermission]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleFormPermissioSet] DROP CONSTRAINT [FK_RoleModulePermission];
GO
IF OBJECT_ID(N'[dbo].[FK_FormModuleForm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FormsModules] DROP CONSTRAINT [FK_FormModuleForm];
GO
IF OBJECT_ID(N'[dbo].[FK_FormModuleModule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FormsModules] DROP CONSTRAINT [FK_FormModuleModule];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonCity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[People] DROP CONSTRAINT [FK_PersonCity];
GO
IF OBJECT_ID(N'[dbo].[FK_CardPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cards] DROP CONSTRAINT [FK_CardPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_AssignmentOrganizationDivision]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Assignments] DROP CONSTRAINT [FK_AssignmentOrganizationDivision];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonAssignment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[People] DROP CONSTRAINT [FK_PersonAssignment];
GO
IF OBJECT_ID(N'[dbo].[FK_OrganizationBranch]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Branchs] DROP CONSTRAINT [FK_OrganizationBranch];
GO
IF OBJECT_ID(N'[dbo].[FK_DivisionBranchDivision]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DivisionsBranchs] DROP CONSTRAINT [FK_DivisionBranchDivision];
GO
IF OBJECT_ID(N'[dbo].[FK_DivisionBranchBranch]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DivisionsBranchs] DROP CONSTRAINT [FK_DivisionBranchBranch];
GO
IF OBJECT_ID(N'[dbo].[FK_AccessPointEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccessPoints] DROP CONSTRAINT [FK_AccessPointEvent];
GO
IF OBJECT_ID(N'[dbo].[FK_AttendanceCard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cards] DROP CONSTRAINT [FK_AttendanceCard];
GO
IF OBJECT_ID(N'[dbo].[FK_AttendanceAccessPoint]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Attendances] DROP CONSTRAINT [FK_AttendanceAccessPoint];
GO
IF OBJECT_ID(N'[dbo].[FK_EventEventType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_EventEventType];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleFormPermissioForm]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleFormPermissioSet] DROP CONSTRAINT [FK_RoleFormPermissioForm];
GO
IF OBJECT_ID(N'[dbo].[FK_CityDepartment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Citys] DROP CONSTRAINT [FK_CityDepartment];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[People]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People];
GO
IF OBJECT_ID(N'[dbo].[Forms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Forms];
GO
IF OBJECT_ID(N'[dbo].[Models]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Models];
GO
IF OBJECT_ID(N'[dbo].[UserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRoles];
GO
IF OBJECT_ID(N'[dbo].[RoleFormPermissioSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleFormPermissioSet];
GO
IF OBJECT_ID(N'[dbo].[Permits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Permits];
GO
IF OBJECT_ID(N'[dbo].[FormsModules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FormsModules];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[Citys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Citys];
GO
IF OBJECT_ID(N'[dbo].[Cards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cards];
GO
IF OBJECT_ID(N'[dbo].[Organizations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Organizations];
GO
IF OBJECT_ID(N'[dbo].[OrganizationDivisions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrganizationDivisions];
GO
IF OBJECT_ID(N'[dbo].[Assignments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Assignments];
GO
IF OBJECT_ID(N'[dbo].[Branchs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Branchs];
GO
IF OBJECT_ID(N'[dbo].[DivisionsBranchs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DivisionsBranchs];
GO
IF OBJECT_ID(N'[dbo].[Events]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Events];
GO
IF OBJECT_ID(N'[dbo].[EventsTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventsTypes];
GO
IF OBJECT_ID(N'[dbo].[AccessPoints]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccessPoints];
GO
IF OBJECT_ID(N'[dbo].[Attendances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Attendances];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL,
    [ActivationCode] nvarchar(max)  NOT NULL,
    [PersonId_Id] int  NOT NULL
);
GO

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [MiddleName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [SecondLastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [DocumentNumber] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [DocumenType] nvarchar(max)  NOT NULL,
    [BlodType] nvarchar(max)  NOT NULL,
    [Photo] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [CityId_Id] int  NOT NULL,
    [AssignmentId_Id] int  NOT NULL
);
GO

-- Creating table 'Forms'
CREATE TABLE [dbo].[Forms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL
);
GO

-- Creating table 'Models'
CREATE TABLE [dbo].[Models] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UserRoles'
CREATE TABLE [dbo].[UserRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] bit  NOT NULL,
    [RoleId_Id] int  NOT NULL,
    [UserId_Id] int  NOT NULL
);
GO

-- Creating table 'RoleFormPermissioSet'
CREATE TABLE [dbo].[RoleFormPermissioSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] bit  NOT NULL,
    [RoleId_Id] int  NOT NULL,
    [PermissionId_Id] int  NOT NULL,
    [FormId_Id] int  NOT NULL
);
GO

-- Creating table 'Permits'
CREATE TABLE [dbo].[Permits] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL
);
GO

-- Creating table 'FormsModules'
CREATE TABLE [dbo].[FormsModules] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] bit  NOT NULL,
    [Form_Id] int  NOT NULL,
    [Module_Id] int  NOT NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Citys'
CREATE TABLE [dbo].[Citys] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [DepartmentId_Id] int  NOT NULL
);
GO

-- Creating table 'Cards'
CREATE TABLE [dbo].[Cards] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QR] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL,
    [ExpirationDate] datetime  NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [AttendanceId] int  NOT NULL,
    [PersonId_Id] int  NOT NULL
);
GO

-- Creating table 'Organizations'
CREATE TABLE [dbo].[Organizations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL
);
GO

-- Creating table 'OrganizationDivisions'
CREATE TABLE [dbo].[OrganizationDivisions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL
);
GO

-- Creating table 'Assignments'
CREATE TABLE [dbo].[Assignments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL,
    [DivisionId_Id] int  NOT NULL
);
GO

-- Creating table 'Branchs'
CREATE TABLE [dbo].[Branchs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [location] nvarchar(max)  NOT NULL,
    [OrganizationId] int  NOT NULL,
    [Status] bit  NOT NULL
);
GO

-- Creating table 'DivisionsBranchs'
CREATE TABLE [dbo].[DivisionsBranchs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] bit  NOT NULL,
    [DivisionId_Id] int  NOT NULL,
    [BranchId_Id] int  NOT NULL
);
GO

-- Creating table 'Events'
CREATE TABLE [dbo].[Events] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL,
    [EventTypeId_Id] int  NOT NULL
);
GO

-- Creating table 'EventsTypes'
CREATE TABLE [dbo].[EventsTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL
);
GO

-- Creating table 'AccessPoints'
CREATE TABLE [dbo].[AccessPoints] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL,
    [EventId_Id] int  NOT NULL
);
GO

-- Creating table 'Attendances'
CREATE TABLE [dbo].[Attendances] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Hour] datetime  NOT NULL,
    [TypeOfRecord] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL,
    [AccessPointId_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Forms'
ALTER TABLE [dbo].[Forms]
ADD CONSTRAINT [PK_Forms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Models'
ALTER TABLE [dbo].[Models]
ADD CONSTRAINT [PK_Models]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [PK_UserRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoleFormPermissioSet'
ALTER TABLE [dbo].[RoleFormPermissioSet]
ADD CONSTRAINT [PK_RoleFormPermissioSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Permits'
ALTER TABLE [dbo].[Permits]
ADD CONSTRAINT [PK_Permits]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FormsModules'
ALTER TABLE [dbo].[FormsModules]
ADD CONSTRAINT [PK_FormsModules]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Citys'
ALTER TABLE [dbo].[Citys]
ADD CONSTRAINT [PK_Citys]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cards'
ALTER TABLE [dbo].[Cards]
ADD CONSTRAINT [PK_Cards]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Organizations'
ALTER TABLE [dbo].[Organizations]
ADD CONSTRAINT [PK_Organizations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrganizationDivisions'
ALTER TABLE [dbo].[OrganizationDivisions]
ADD CONSTRAINT [PK_OrganizationDivisions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Assignments'
ALTER TABLE [dbo].[Assignments]
ADD CONSTRAINT [PK_Assignments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Branchs'
ALTER TABLE [dbo].[Branchs]
ADD CONSTRAINT [PK_Branchs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DivisionsBranchs'
ALTER TABLE [dbo].[DivisionsBranchs]
ADD CONSTRAINT [PK_DivisionsBranchs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [PK_Events]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventsTypes'
ALTER TABLE [dbo].[EventsTypes]
ADD CONSTRAINT [PK_EventsTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AccessPoints'
ALTER TABLE [dbo].[AccessPoints]
ADD CONSTRAINT [PK_AccessPoints]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Attendances'
ALTER TABLE [dbo].[Attendances]
ADD CONSTRAINT [PK_Attendances]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PersonId_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_UserPerson]
    FOREIGN KEY ([PersonId_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPerson'
CREATE INDEX [IX_FK_UserPerson]
ON [dbo].[Users]
    ([PersonId_Id]);
GO

-- Creating foreign key on [RoleId_Id] in table 'RoleFormPermissioSet'
ALTER TABLE [dbo].[RoleFormPermissioSet]
ADD CONSTRAINT [FK_RoleModuleRole]
    FOREIGN KEY ([RoleId_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleModuleRole'
CREATE INDEX [IX_FK_RoleModuleRole]
ON [dbo].[RoleFormPermissioSet]
    ([RoleId_Id]);
GO

-- Creating foreign key on [RoleId_Id] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserRoleRole]
    FOREIGN KEY ([RoleId_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRoleRole'
CREATE INDEX [IX_FK_UserRoleRole]
ON [dbo].[UserRoles]
    ([RoleId_Id]);
GO

-- Creating foreign key on [UserId_Id] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserRoleUser]
    FOREIGN KEY ([UserId_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRoleUser'
CREATE INDEX [IX_FK_UserRoleUser]
ON [dbo].[UserRoles]
    ([UserId_Id]);
GO

-- Creating foreign key on [PermissionId_Id] in table 'RoleFormPermissioSet'
ALTER TABLE [dbo].[RoleFormPermissioSet]
ADD CONSTRAINT [FK_RoleModulePermission]
    FOREIGN KEY ([PermissionId_Id])
    REFERENCES [dbo].[Permits]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleModulePermission'
CREATE INDEX [IX_FK_RoleModulePermission]
ON [dbo].[RoleFormPermissioSet]
    ([PermissionId_Id]);
GO

-- Creating foreign key on [Form_Id] in table 'FormsModules'
ALTER TABLE [dbo].[FormsModules]
ADD CONSTRAINT [FK_FormModuleForm]
    FOREIGN KEY ([Form_Id])
    REFERENCES [dbo].[Forms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FormModuleForm'
CREATE INDEX [IX_FK_FormModuleForm]
ON [dbo].[FormsModules]
    ([Form_Id]);
GO

-- Creating foreign key on [Module_Id] in table 'FormsModules'
ALTER TABLE [dbo].[FormsModules]
ADD CONSTRAINT [FK_FormModuleModule]
    FOREIGN KEY ([Module_Id])
    REFERENCES [dbo].[Models]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FormModuleModule'
CREATE INDEX [IX_FK_FormModuleModule]
ON [dbo].[FormsModules]
    ([Module_Id]);
GO

-- Creating foreign key on [CityId_Id] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [FK_PersonCity]
    FOREIGN KEY ([CityId_Id])
    REFERENCES [dbo].[Citys]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonCity'
CREATE INDEX [IX_FK_PersonCity]
ON [dbo].[People]
    ([CityId_Id]);
GO

-- Creating foreign key on [PersonId_Id] in table 'Cards'
ALTER TABLE [dbo].[Cards]
ADD CONSTRAINT [FK_CardPerson]
    FOREIGN KEY ([PersonId_Id])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CardPerson'
CREATE INDEX [IX_FK_CardPerson]
ON [dbo].[Cards]
    ([PersonId_Id]);
GO

-- Creating foreign key on [DivisionId_Id] in table 'Assignments'
ALTER TABLE [dbo].[Assignments]
ADD CONSTRAINT [FK_AssignmentOrganizationDivision]
    FOREIGN KEY ([DivisionId_Id])
    REFERENCES [dbo].[OrganizationDivisions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AssignmentOrganizationDivision'
CREATE INDEX [IX_FK_AssignmentOrganizationDivision]
ON [dbo].[Assignments]
    ([DivisionId_Id]);
GO

-- Creating foreign key on [AssignmentId_Id] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [FK_PersonAssignment]
    FOREIGN KEY ([AssignmentId_Id])
    REFERENCES [dbo].[Assignments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonAssignment'
CREATE INDEX [IX_FK_PersonAssignment]
ON [dbo].[People]
    ([AssignmentId_Id]);
GO

-- Creating foreign key on [OrganizationId] in table 'Branchs'
ALTER TABLE [dbo].[Branchs]
ADD CONSTRAINT [FK_OrganizationBranch]
    FOREIGN KEY ([OrganizationId])
    REFERENCES [dbo].[Organizations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrganizationBranch'
CREATE INDEX [IX_FK_OrganizationBranch]
ON [dbo].[Branchs]
    ([OrganizationId]);
GO

-- Creating foreign key on [DivisionId_Id] in table 'DivisionsBranchs'
ALTER TABLE [dbo].[DivisionsBranchs]
ADD CONSTRAINT [FK_DivisionBranchDivision]
    FOREIGN KEY ([DivisionId_Id])
    REFERENCES [dbo].[OrganizationDivisions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DivisionBranchDivision'
CREATE INDEX [IX_FK_DivisionBranchDivision]
ON [dbo].[DivisionsBranchs]
    ([DivisionId_Id]);
GO

-- Creating foreign key on [BranchId_Id] in table 'DivisionsBranchs'
ALTER TABLE [dbo].[DivisionsBranchs]
ADD CONSTRAINT [FK_DivisionBranchBranch]
    FOREIGN KEY ([BranchId_Id])
    REFERENCES [dbo].[Branchs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DivisionBranchBranch'
CREATE INDEX [IX_FK_DivisionBranchBranch]
ON [dbo].[DivisionsBranchs]
    ([BranchId_Id]);
GO

-- Creating foreign key on [EventId_Id] in table 'AccessPoints'
ALTER TABLE [dbo].[AccessPoints]
ADD CONSTRAINT [FK_AccessPointEvent]
    FOREIGN KEY ([EventId_Id])
    REFERENCES [dbo].[Events]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccessPointEvent'
CREATE INDEX [IX_FK_AccessPointEvent]
ON [dbo].[AccessPoints]
    ([EventId_Id]);
GO

-- Creating foreign key on [AccessPointId_Id] in table 'Attendances'
ALTER TABLE [dbo].[Attendances]
ADD CONSTRAINT [FK_AttendanceAccessPoint]
    FOREIGN KEY ([AccessPointId_Id])
    REFERENCES [dbo].[AccessPoints]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AttendanceAccessPoint'
CREATE INDEX [IX_FK_AttendanceAccessPoint]
ON [dbo].[Attendances]
    ([AccessPointId_Id]);
GO

-- Creating foreign key on [EventTypeId_Id] in table 'Events'
ALTER TABLE [dbo].[Events]
ADD CONSTRAINT [FK_EventEventType]
    FOREIGN KEY ([EventTypeId_Id])
    REFERENCES [dbo].[EventsTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventEventType'
CREATE INDEX [IX_FK_EventEventType]
ON [dbo].[Events]
    ([EventTypeId_Id]);
GO

-- Creating foreign key on [FormId_Id] in table 'RoleFormPermissioSet'
ALTER TABLE [dbo].[RoleFormPermissioSet]
ADD CONSTRAINT [FK_RoleFormPermissioForm]
    FOREIGN KEY ([FormId_Id])
    REFERENCES [dbo].[Forms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleFormPermissioForm'
CREATE INDEX [IX_FK_RoleFormPermissioForm]
ON [dbo].[RoleFormPermissioSet]
    ([FormId_Id]);
GO

-- Creating foreign key on [DepartmentId_Id] in table 'Citys'
ALTER TABLE [dbo].[Citys]
ADD CONSTRAINT [FK_CityDepartment]
    FOREIGN KEY ([DepartmentId_Id])
    REFERENCES [dbo].[Departments]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CityDepartment'
CREATE INDEX [IX_FK_CityDepartment]
ON [dbo].[Citys]
    ([DepartmentId_Id]);
GO

-- Creating foreign key on [AttendanceId] in table 'Cards'
ALTER TABLE [dbo].[Cards]
ADD CONSTRAINT [FK_AttendanceCard]
    FOREIGN KEY ([AttendanceId])
    REFERENCES [dbo].[Attendances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AttendanceCard'
CREATE INDEX [IX_FK_AttendanceCard]
ON [dbo].[Cards]
    ([AttendanceId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------