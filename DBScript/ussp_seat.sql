DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ussp_seat`(
p_seatid int,
p_seatlabel varchar(50),
p_floorid int,
p_booked tinyint
)
BEGIN
select s.`seatid`, `seatlabel`, `cubicalno`, 
		`rowno`, `columnno`, `colspan`, 
        `rowspan`, s.`teamid`, `floorid`, `type`,
        case when (allocationid is not null or (l.seatvacancyid is null and e.seatno is not null))  then 1 else 0 end as `isbooked`,
        e.associateno, e.name
        from seat s left join 
        seatallocation a on s.seatid=a.seatid  and cast(now() as date) between a.startdate and a.enddate
        left join associate e on s.seatlabel=e.seatno
        left join seatvacancy l on e.associateno=l.associateno
				and cast(now() as date) between l.startdate and l.enddate 
        where  (s.seatid=p_seatid or p_seatid  is null)
	        AND(s.seatlabel=p_seatlabel or p_seatlabel is null)
			AND(s.floorid=p_floorid or p_floorid is null)
	    order by s.rowno, s.columnno;

END$$
DELIMITER ;
