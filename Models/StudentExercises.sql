/*
DROP TABLE StudentExercises;
DROP TABLE Exercise;
DROP TABLE Student;
DROP TABLE Instructor;
DROP TABLE Cohort;
*/


CREATE TABLE Exercise (
	Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	Name TEXT NOT NULL,
	`Language` TEXT NOT NULL
);

CREATE TABLE Cohort (
	Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	Name TEXT NOT NULL
);

CREATE TABLE Instructor (
	Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	FirstName TEXT NOT NULL,
	LastName TEXT NOT NULL,
	SlackHandle TEXT NOT NULL,
	CohortId INTEGER NOT NULL,
	FOREIGN KEY (CohortID) REFERENCES Cohort(ID)
);

CREATE TABLE Student (
	Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	FirstName TEXT NOT NULL,
	LastName TEXT NOT NULL,
	SlackHandle TEXT NOT NULL,
	CohortId INTEGER NOT NULL,
	FOREIGN KEY (CohortID) REFERENCES Cohort(ID)
);

CREATE TABLE StudentExercises (
	Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	StudentId INTEGER NOT NULL,
	ExerciseId INTEGER NOT NULL,
	FOREIGN KEY (StudentId) REFERENCES Student(ID),
	FOREIGN KEY (ExerciseId) REFERENCES Exercise(ID)
);


INSERT INTO Exercise (name, `language`) VALUES ('Ex 1', 'javascript');
INSERT INTO Exercise (name, `language`) VALUES ('Ex 2', 'csharp');
INSERT INTO Exercise (name, `language`) VALUES ('Ex 3', 'python');
INSERT INTO Exercise (name, `language`) VALUES ('Ex 4', 'javascript');

INSERT INTO Cohort (name) VALUES ('Day 27');
INSERT INTO Cohort (name) VALUES ('Day 26');
INSERT INTO Cohort (name) VALUES ('Evening 7');

INSERT INTO Instructor ( FirstName, LastName, SlackHandle, CohortId) 
    VALUES ( 'InstFirst1', 'InstLast1', 'inst1', 1);
INSERT INTO Instructor ( FirstName, LastName, SlackHandle, CohortId) 
    VALUES ( 'InstFirst2', 'InstLast2', 'inst2', 2);
INSERT INTO Instructor ( FirstName, LastName, SlackHandle, CohortId) 
    VALUES ( 'InstFirst2', 'InstLast2', 'inst2', 2);

INSERT INTO Student ( FirstName, LastName, SlackHandle, CohortId) 
    VALUES ( 'StudFirst1', 'StudLast1', 'stud1', 1);
INSERT INTO Student ( FirstName, LastName, SlackHandle, CohortId) 
    VALUES ( 'StudFirst2', 'StudLast2', 'stud2', 2);
INSERT INTO Student ( FirstName, LastName, SlackHandle, CohortId) 
    VALUES ( 'StudFirst2', 'StudLast2', 'stud2', 2);

INSERT INTO StudentExercises (StudentId, ExerciseId) VALUES (1, 1);
INSERT INTO StudentExercises (StudentId, ExerciseId) VALUES (1, 2);
INSERT INTO StudentExercises (StudentId, ExerciseId) VALUES (1, 3);
INSERT INTO StudentExercises (StudentId, ExerciseId) VALUES (1, 4);

INSERT INTO StudentExercises (StudentId, ExerciseId) VALUES (2, 1);