SELECT 
	E.id																												, 
	FORMAT(E.amount, 'C', 'pt-br')																	[amountFormat]		,
	E.fine																												,
	E.interest																											,
	E.discount																											,
	E.qtyInstallment																									,
	E.description																										,
	E.installment																										,
	CONVERT(varchar(10),E.dateCreated,103)															[dateCreatedFormat]	,
	CASE WHEN E.dueDate IS NOT NULL THEN CONVERT(varchar(10),E.dueDate,103) ELSE '-' END			[dueDateFormat]		,
	CASE WHEN e.paymentDate IS NOT NULL THEN CONVERT(varchar(10),e.paymentDate,103) ELSE '-' END	[paymentDateFormat]	,
	REC.description																					[expenseCategory]	,
	REC.color																											,
	RPM.name																						[paymentMethod]		,
	RD.name																							[department]		,
	RA.name																							[account]			,
	ES.description																					[status]			,
	UPPER(ET.description)																			[expenseType]		
FROM		[Expense]					E	 (NOLOCK)
INNER JOIN	[Register.Expense_Category]	REC	 (NOLOCK) ON (REC.id  = E.idExpenseCategory)
INNER JOIN	[Register.Payment_Method]	RPM  (NOLOCK) ON (RPM.id  = E.idPaymentMethod)
INNER JOIN	[Register.Department]		RD	(NOLOCK) ON (RD.id	  = E.idDepartment)
INNER JOIN	[Register.Account]			RA	(NOLOCK) ON (RA.id	  = E.idAccount)
INNER JOIN	[Expense.Expense_Status]	ES	(NOLOCK) ON (ES.id	  = E.idExpenseStatus)
INNER JOIN	[Expense.Expense_Type]		ET	(NOLOCK) ON (ET.id	  = E.idExpenseType)

