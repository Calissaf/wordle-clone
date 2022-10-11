CREATE DATABASE IF NOT EXISTS wordList;

USE wordList;


CREATE TABLE words (
    id INT PRIMARY KEY AUTO_INCREMENT,
    word VARCHAR(5) NOT NULL
);

SHOW TABLES;
DESCRIBE words;

INSERT INTO words (word)
VALUES 
('aback'),
('abase'),
('abate'),
('abbey'),
('abbot');

SELECT *
FROM words;

SELECT COUNT(id) FROM words;

