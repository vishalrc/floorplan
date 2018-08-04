DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ussp_dashboard`(
p_floorid int
)
BEGIN
	declare v_totalseat int ;
    declare v_occupiedseat int ;
    declare v_vacantseat int;
    declare v_seatavailablemorethan10days int;
    declare v_seatavailablemorethan5days int;
    declare v_seatavailablelessthan5days int;
    
	select count(0) into v_totalseat from seat  where floorid=p_floorid;
    
    select count(0) into v_occupiedseat from seat s join associate a on s.seatlabel=a.seatno
		where floorid=p_floorid;
        
	select count(0) into v_seatavailablemorethan10days from seat s join associate a on s.seatlabel=a.seatno
         join seatvacancy l 
		on a.associateno=l.associateno where datediff(now(),enddate)>=9 
		and floorid=p_floorid;
        
    select count(0) into v_seatavailablemorethan5days from seat s join associate a on s.seatlabel=a.seatno
         join seatvacancy l 
		on a.associateno=l.associateno where datediff(now(),enddate)>=5 
		and floorid=p_floorid;
        
	select count(0) into v_seatavailablelessthan5days from seat s join associate a on s.seatlabel=a.seatno
         join seatvacancy l 
		on a.associateno=l.associateno where datediff(now(),enddate)<5 
		and floorid=p_floorid;
        
	select v_totalseat totalseat,v_occupiedseat occupiedseat,(v_totalseat-v_occupiedseat) vacantseat,
			v_seatavailablemorethan10days+(v_totalseat-v_occupiedseat) seatavailablemorethan10days,v_seatavailablemorethan5days seatavailablemorethan5days,
			v_seatavailablelessthan5days seatavailablelessthan5days;
END$$
DELIMITER ;
