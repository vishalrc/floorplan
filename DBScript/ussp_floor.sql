DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ussp_floor`(
p_floorid int,
p_floorname varchar(200),
p_buildingname varchar(200),
p_isactive tinyint(1)
)
BEGIN 
	
	SELECT floorid, floorname, buildingname, buildingcode	FROM bldgfloor f join building b 
    on f.buildingid=b.buildingid
	where (f.floorid=p_floorid or p_floorid is null)
	AND(f.floorname=p_floorname or p_floorname is null)
    AND(b.buildingname=p_buildingname or p_buildingname is null)
	AND (f.isactive = p_isactive or p_isactive is null);
END$$
DELIMITER ;
