DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ussp_seat`(
p_seatid int,
p_seatlabel varchar(50),
p_booked tinyint
)
BEGIN
select `seatid`, `seatlabel`, `cubicalno`, 
		`rowno`, `columnno`, `colspan`, 
        `rowspan`, `teamid`, `floorid`, `type`, 0 as `isbooked`
        from seat s
        where (s.seatid=p_seatid or p_seatid  is null)
	          AND(s.seatlabel=p_seatlabel or p_seatlabel is null);

END$$
DELIMITER ;
