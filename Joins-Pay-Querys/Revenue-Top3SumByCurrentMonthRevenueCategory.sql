DECLARE @monthCurrent AS int
SET @monthCurrent = DATEPART(MONTH, GETDATE());
SELECT 
	TOP(3) R.idRevenueCategory, 
	RRC.description	AS [descriptionRevenueCategory], 
	SUM(R.amount)	AS [totalAmountRevenueCategory]
FROM Revenue R (NOLOCK)
INNER JOIN [Register.Revenue_Category] RRC ON (RRC.id = R.idRevenueCategory)
WHERE DATEPART(MONTH, R.dateCreated) = @monthCurrent
GROUP BY RRC.description, R.idRevenueCategory
ORDER BY [totalAmountRevenueCategory] DESC