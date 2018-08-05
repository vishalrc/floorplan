DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `usp_processleavexml`( 
p_leavexml text /* <es><e uid="" fn="" ln="" em=""/></es> */
)
BEGIN
declare t_rowcount int;
declare t_rowindex int unsigned default 0;

drop table if exists tbl_leavexml; 
CREATE TEMPORARY TABLE tbl_leavexml(id int(11), uniqueid varchar(50),firstname varchar(200),lastname varchar(200),emailid varchar(200)); 
set t_rowcount = extractValue(p_leavexml, 'count(/es/e)'); 

 while t_rowindex < t_rowcount do                
        set t_rowindex = t_rowindex + 1; 
        insert into tbl_leavexml 
        values (t_rowindex,
			extractValue(p_leavexml, concat('/es/e[',t_rowindex,']/@*[1]')),
            extractValue(p_leavexml, concat('/es/e[',t_rowindex,']/@*[2]')),
            extractValue(p_leavexml, concat('/es/e[',t_rowindex,']/@*[3]')),
            extractValue(p_leavexml, concat('/es/e[',t_rowindex,']/@*[4]'))
        );
    end while;
select * from tbl_p_leavexml;
END$$
DELIMITER ;
