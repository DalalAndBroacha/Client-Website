/*
select	b.Login_Name as family_name, a.main_client_id,
	isnull('Client : ' + d.Name + ' (' + d.pan_no + ')', 'All Clients') as main_client_name,
	isnull('' + d.Name, 'All Clients') as main_client_name_header,
	isnull(d.pan_no, '') as pan_number,
	c.account_code, c.id as client_id,
	(case when category = 'Equity' then 1 when  category = 'Debt' then 2 when category = 'AIF' then 4
		when category = 'PMS' then 5 when category = 'InvITs & ReITs' then 7 when category = 'OTHER PMS' then 10 when category is null then 11 else 12 end) reord_order,
	upper(category), sub_category, sub_category + ' - ' + c.account_code as sub_category_display,
	1 sub_category_display_order,
		contribution as contribution,
		--(hld_cost + cash_bank) as hld_cost,
		(hld_cost) as hld_cost,
		--(Market_Value + cash_bank) as Market_Value,
		(Market_Value) as Market_Value,
		--convert(decimal(22, 2), (case when d.total_Market_Value = 0 then 0 else(Market_Value + cash_bank) / d.total_Market_Value * 100 end)) as holding_per,
			convert(decimal(22, 2), (case when d.total_Market_Value = 0 then 0 else(Market_Value) / d.total_Market_Value * 100 end)) as holding_per,
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
inner join dw_mst_all_client c
on a.client_id = c.Id
inner join #tmp_main_client_id_mkt_value d
on a.main_client_id = d.main_client_id
where a.family_id = @Family and fin_year = @FINYR
	and LTRIM(RTRIM(a.category)) <> '' and client_id <> 0 and a.main_client_id <> 0
	and a.sub_category in ('Equity PMS', 'Direct Equity', 'Liqui Loan', 'InvITs', 'ReITs', 'Emerging Corporates India Portfolio', 'Moat And Special Situations Portfolio',
	'Marcellus Consistent Compounders Portfolio', 'Marcellus Little Champs Portfolio', 'Marcellus Kings Of Capital Portfolio', 'Buoyant Opportunities Scheme', 'Buoyant Opportunities Strategy - Investor',
	'GIRIK MULTICAP GROWTH EQUITY STRATEGY', 'GIRIK LIQUID STRATEGY', 'InCred Healthcare Portfolio', 'WHITE OAK INDIA PIONEERS EQUITY PORTFOLIO', 'Fixed Deposit') and sub_category <> ''
	*/