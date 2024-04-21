// eslint-disable-next-line no-unused-vars
import React from "react";
import { Grid, Label, Icon } from "semantic-ui-react";
//import { usePaginatedIncomes } from "../../app/hooks/usePaginatedIncomes"

export default function IncomesItems() {
  //const { paginatedIncomes } = usePaginatedIncomes();

  const paginatedIncomes = [
    { id: 1, incomeDate: "2024-04-15 15:00", amount: 1000 },
    { id: 2, incomeDate: "2024-04-16", amount: 1500 },
    { id: 3, incomeDate: "2024-04-17", amount: 1200 },
  ];

  const renderedIncomes =
    paginatedIncomes &&
    paginatedIncomes.map((income) => (
      <Grid.Row
        stretched
        padded="horizontally"
        columns={4}
        verticalAlign="middle"
        className="hoverable-row"
        key={income.id}
      >
        <Grid.Column width={3}>
          <h4>
            <Label circular horizontal size="big" color="green">
              <Icon fitted={true} name="money bill alternate outline" />
            </Label>
            Salary
          </h4>
        </Grid.Column>
        <Grid.Column width={4}>
          <h4>{income.incomeDate}</h4>
        </Grid.Column>
        <Grid.Column className="overflow-ellipsis" width={5}>
          AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        </Grid.Column>
        <Grid.Column width={2}>
          <h4 className="income-amount">+{income.amount}</h4>
        </Grid.Column>
      </Grid.Row>
    ));

  return (
    <>
      <Grid container centered relaxed>
        {renderedIncomes}
      </Grid>
    </>
  );
}
