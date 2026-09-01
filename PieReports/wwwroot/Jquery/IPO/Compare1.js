/*
select	b.Login_Name as family_name, a.main_client_id, 
		isnull('Client : ' + c.Name + ' (' + c.pan_no + ')', 'All Clients') as main_client_name, 
		isnull('' + c.Name, 'All Clients') as main_client_name_header, 
		isnull(c.pan_no, '') as pan_number,  space(20) as account_code, 0 as client_id, 
		(case when category = 'Equity' then 1 when  category ='Debt' then 2 when category = 'AIF' then 4
		when category = 'PMS' then 5 when category = 'InvITs & ReITs' then 7 when category = 'OTHER PMS' then 10 else 11 end) reord_order,
		upper(category), sub_category, sub_category as sub_category_display,
		1 sub_category_display_order,
		contribution as contribution,
		--(hld_cost + cash_bank) as hld_cost,
		(hld_cost) as hld_cost,
		--(Market_Value + cash_bank) as Market_Value, 
		(Market_Value) as Market_Value, 
		holding_per, 
		dividend dividend, 
		srt_unreal_profit,
		long_unreal_profit,
		srt_real_profit, 
		long_real_profit, 
		srt_unreal_profit + srt_real_profit as srt_ttl_gain, 
		long_unreal_profit + long_real_profit as lng_ttl_gain, 
		(case when a.main_client_id = 0 then 0 else return_abs end) as return_abs, 
		(case when a.main_client_id = 0 then 0 else return_xirr end) as return_xirr, 
		0 as net_return_abs, 0 as net_return_xirr
from dw_trn_portfolio_performance_detail_family a
inner join dw_mst_user b
on a.family_id = b.Id
left outer join dw_mst_client c
on a.main_client_id = c.ID
where a.family_id = @Family and fin_year = @FINYR
	and LTRIM(RTRIM(a.category)) <> '' and client_id = 0 and main_client_id <> 0
	and a.sub_category not in ('Equity PMS', 'Direct Equity', 'Liqui Loan', 'InvITs', 'ReITs', 'Emerging Corporates India Portfolio', 'Moat And Special Situations Portfolio',
	'Marcellus Consistent Compounders Portfolio', 'Marcellus Little Champs Portfolio', 'Marcellus Kings Of Capital Portfolio', 'Buoyant Opportunities Scheme', 'Buoyant Opportunities Strategy - Investor',
	'GIRIK MULTICAP GROWTH EQUITY STRATEGY','GIRIK LIQUID STRATEGY','InCred Healthcare Portfolio', 'WHITE OAK INDIA PIONEERS EQUITY PORTFOLIO', 'Fixed Deposit') and sub_category <> ''
*/