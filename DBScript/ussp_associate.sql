DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ussp_associate`(
p_associateid int,
p_associateno int,
p_departmentid int,
p_departmentname varchar(50),
p_name varchar(50),
p_isactive tinyint(1)
)
BEGIN
SELECT a.`associateid`,
    a.`associateno`,
    a.`name`,
    a.`dob`,
    a.`phoneno`,
    a.`emailid`,
    a.`Profile`,
    a.`teamid`,
    a.`departmentid`,
    d.`departmentname`,
    a.`seatno`,
    a.`Photo`,
    a.`isactive`,
    case when l.seatvacancyid is null then 0 else 1 end `onleave`
FROM `associate` a join `department` d 
on a.departmentid=d.departmentid
left join  seatvacancy l on l.associateno=a.associateno and now() between l.startdate and l.enddate
Where (a.associateid=p_associateid or p_associateid  is null)
and (a.associateno=p_associateno or p_associateno is null)
and (a.name like concat('%' , p_name , '%') or p_name is null)
and (a.departmentid=p_departmentid or p_departmentid is null)
and (d.departmentname=p_departmentname or p_departmentname is null)
and (a.isactive=p_isactive or p_isactive is null);


END$$
DELIMITER ;
