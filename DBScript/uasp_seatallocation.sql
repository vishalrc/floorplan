DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `uasp_seatallocation`(
p_allocationid int,
p_seatid int,
p_employeeno int,
p_startdate varchar(20),
p_enddate varchar(20)
)
BEGIN 
	
    if not exists(select allocationid from seatallocation where seatid=p_seatid and ((STR_TO_DATE(p_startdate, '%d/%m/%Y') between startdate and enddate)
    or (STR_TO_DATE(p_enddate, '%d/%m/%Y') between startdate and enddate))) then
		INSERT INTO `seatallocation`
			(`associateid`,
			`seatid`,
			`startdate`,
			`enddate`,
			`days`,
			`isactive`)
			VALUES
			(p_employeeno,
			p_seatid,
			STR_TO_DATE(p_startdate, '%d/%m/%Y'),
			STR_TO_DATE(p_enddate, '%d/%m/%Y'),
			datediff(STR_TO_DATE(p_enddate, '%d/%m/%Y'),STR_TO_DATE(p_startdate, '%d/%m/%Y')) ,
			1
			);
	else
		call raise_error(30006,'Error!: seat is not available for selected date range '); 
	end if;
	
END$$
DELIMITER ;
