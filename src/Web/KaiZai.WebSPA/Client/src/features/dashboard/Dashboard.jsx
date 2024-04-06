import {
  Icon,
  Label,
  List,
  Grid,
  Divider,
  GridRow,
  GridColumn,
} from "semantic-ui-react";
import {
  BarChart,
  Bar,
  LineChart, 
  Line,
  XAxis,
  YAxis,
  Tooltip,
  Legend,
  CartesianGrid,
  ResponsiveContainer,
} from "recharts";
import DatePicker from "react-datepicker";
import { useState } from "react";

export default function Dashboard() {
  const data = [
    {
      name: "January",
      Incomes: 4000,
      Expenses: 2400,
      amt: 2400,
    },
    {
      name: "February",
      Incomes: 3000,
      Expenses: 1398,
      amt: 2210,
    },
    {
      name: "March",
      Incomes: 9000,
      Expenses: 4800,
      amt: 2290,
    },
    {
      name: "April",
      Incomes: 2780,
      Expenses: 3908,
      amt: 2000,
    }
  ];

  const data1 = [
    {
      name: "1-7 March",
      Expenses: 2400,
      amt: 2400,
    },
    {
      name: "8-14 March",
      Expenses: 1398,
      amt: 2210,
    },
    {
      name: "15-21 March",
      Expenses: 4800,
      amt: 2290,
    },
    {
      name: "22-28 March",
      Expenses: 4800,
      amt: 2290,
    }
  ];
  const [startDate, setStartDate] = useState(new Date());

  return (
    <>
      <Grid stackable container>
        <GridRow columns={2} verticalAlign="middle">
          <GridColumn tablet={7} computer={4} largeScreen={5}>
            <div className="feature-content-section">
            <List size="large" relaxed={{ relaxed: true }} verticalAlign="middle">
                <List.Header verticalAlign="left">
                  <h1>Recent transactions</h1>
                  <h3>Expenses & Incomes</h3>
                </List.Header>
                <Divider/>

                <List.Item>
                  <List.Content
                    floated="right"
                    className="expense-amount relative-vertical-centered"
                  >
                    455$
                  </List.Content>
                  <List.Header>
                    <Label circular horizontal size="big" color="blue">
                      <Icon
                        fitted={true}
                        size="small"
                        name="money bill alternate outline"
                      />
                    </Label>
                    Salary
                  </List.Header>
                </List.Item>
                <List.Item>
                  <List.Content
                    floated="right"
                    className="expense-amount relative-vertical-centered"
                  >
                    455$
                  </List.Content>
                  <List.Header>
                    <Label circular horizontal size="big" color="blue">
                      <Icon
                        fitted={true}
                        size="small"
                        name="money bill alternate outline"
                      />
                    </Label>
                    Salary
                  </List.Header>
                </List.Item>
                <List.Item>
                  <List.Content
                    floated="right"
                    className="expense-amount relative-vertical-centered"
                  >
                    255$
                  </List.Content>
                  <List.Header>
                    <Label circular horizontal size="big" color="green">
                      <Icon
                        fitted={true}
                        size="small"
                        name="money bill alternate outline"
                      />
                    </Label>
                    Salary
                  </List.Header>
                </List.Item>
                <List.Item>
                  <List.Content
                    floated="right"
                    className="expense-amount relative-vertical-centered"
                  >
                    255$
                  </List.Content>
                  <List.Header>
                    <Label circular horizontal size="big" color="green">
                      <Icon
                        fitted={true}
                        size="small"
                        name="money bill alternate outline"
                      />
                    </Label>
                    Salary
                  </List.Header>
                </List.Item>
                <List.Item>
                  <List.Content
                      floated="right"
                      className="expense-amount relative-vertical-centered"
                  >
                    255$
                  </List.Content>
                  <List.Header>
                    <Label circular horizontal size="big" color="green">
                      <Icon
                          fitted={true}
                          size="small"
                          name="money bill alternate outline"
                      />
                    </Label>
                    Salary
                  </List.Header>
                </List.Item>
              </List>
            </div>
          </GridColumn>
          <GridColumn width={7} tablet={14} computer={12} mobile={12}>
            <div className="feature-content-section">
            <DatePicker
            selected={startDate}
            onChange={(date) => setStartDate(date)}
            showYearPicker
            dateFormat="yyyy"
            yearItemNumber={9}
              />
              <Divider hidden/>
              <ResponsiveContainer height={340}>
                <LineChart
                  data={data}
                  margin={{
                    top: 5,
                    right: 30,
                    left: 20,
                    bottom: 5,
                  }}
                >
                  <XAxis
                    tick={{ stroke: "#f2eef4", strokeWidth: 0.6 }}
                    dataKey="name"
                  />
                  <YAxis tick={{ stroke: "#f2eef4", strokeWidth: 0.6 }} />
                  <Tooltip />
                  <Legend />
                  <Line
                    type="monotone"
                    dataKey="Incomes"
                    stroke="#4ef745"
                    activeDot={{ r: 8 }}
                  />
                  <Line type="monotone" dataKey="Expenses" stroke="#f74595" />
                </LineChart>
              </ResponsiveContainer>
            </div>
          </GridColumn>
        </GridRow>
        <Grid.Row columns={2}>
          <GridColumn tablet={7} computer={4} largeScreen={5}>
            <div className="feature-content-section">
              <List size="large" relaxed={{ relaxed: true }} verticalAlign="middle">
                <List.Header verticalAlign="left">
                  <h1>Top Expenses</h1>
                  <h3>Last 3 months</h3>
                </List.Header>
                <Divider/>

                <List.Item>
                  <List.Content
                    floated="right"
                    className="expense-amount relative-vertical-centered"
                  >
                    455$
                  </List.Content>
                  <List.Header>
                    <Label circular horizontal size="big" color="blue">
                      <Icon
                        fitted={true}
                        size="small"
                        name="money bill alternate outline"
                      />
                    </Label>
                    Salary
                  </List.Header>
                </List.Item>
                <List.Item>
                  <List.Content
                    floated="right"
                    className="expense-amount relative-vertical-centered"
                  >
                    455$
                  </List.Content>
                  <List.Header>
                    <Label circular horizontal size="big" color="blue">
                      <Icon
                        fitted={true}
                        size="small"
                        name="money bill alternate outline"
                      />
                    </Label>
                    Salary
                  </List.Header>
                </List.Item>
                <List.Item>
                  <List.Content
                    floated="right"
                    className="expense-amount relative-vertical-centered"
                  >
                    255$
                  </List.Content>
                  <List.Header>
                    <Label circular horizontal size="big" color="green">
                      <Icon
                        fitted={true}
                        size="small"
                        name="money bill alternate outline"
                      />
                    </Label>
                    Salary
                  </List.Header>
                </List.Item>
                <List.Item>
                  <List.Content
                    floated="right"
                    className="expense-amount relative-vertical-centered"
                  >
                    255$
                  </List.Content>
                  <List.Header>
                    <Label circular horizontal size="big" color="green">
                      <Icon
                        fitted={true}
                        size="small"
                        name="money bill alternate outline"
                      />
                    </Label>
                    Salary
                  </List.Header>
                </List.Item>
                <List.Item>
                  <List.Content
                      floated="right"
                      className="expense-amount relative-vertical-centered"
                  >
                    255$
                  </List.Content>
                  <List.Header>
                    <Label circular horizontal size="big" color="green">
                      <Icon
                          fitted={true}
                          size="small"
                          name="money bill alternate outline"
                      />
                    </Label>
                    Salary
                  </List.Header>
                </List.Item>
              </List>
            </div>
          </GridColumn>
          <Grid.Column width={16} tablet={14} mobile={12} computer={12}>
        <div className="feature-content-section">
              <h2>Your spendings</h2>
              <h3>Last 4 weeks</h3>
              <Divider hidden />
              <ResponsiveContainer height={340}>
                <BarChart
                  width={500}
                  height={400}
                  data={data1}
                  margin={{
                    top: 5,
                    right: 30,
                    left: 20,
                    bottom: 5,
                  }}
                >
                  <CartesianGrid strokeDasharray="3 3"/>
                  <XAxis
                    tick={{ stroke: "#f2eef4", strokeWidth: 0.6 }}
                    dataKey="name"
                  />
                  <YAxis tick={{ stroke: "#f2eef4", strokeWidth: 0.6 }} />
                  <Tooltip />
                  <Legend />
                  <Bar stackId="a" dataKey="Expenses" fill="#f74595" />
                </BarChart>
              </ResponsiveContainer>
            </div>
          </Grid.Column>
          
        </Grid.Row>
      </Grid>
    </>
  );
}
