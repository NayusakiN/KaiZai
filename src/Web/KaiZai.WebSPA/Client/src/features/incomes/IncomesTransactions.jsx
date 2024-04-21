/* eslint-disable no-unused-vars */
// eslint-disable-next-line no-unused-vars
import React, { useState } from "react";

import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useDispatch, useSelector } from "react-redux";
import { setPageNumber } from "./incomesSlice";
import {
  Pagination,
  Divider,
  SegmentGroup,
  Segment,
  GridRow,
  GridColumn,
  Grid,
  ListItem,
  ListHeader,
  ListContent,
  Image,
  Icon,
  Label,
  List,
  ListDescription,
} from "semantic-ui-react";
import IncomesItems from "./IncomesItems";
import { format, subMonths, startOfMonth, isFirstDayOfMonth } from "date-fns";
import {
  PieChart,
  Tooltip,
  Pie,
  Sector,
  Cell,
  XAxis,
  ResponsiveContainer,
} from "recharts";

export default function IncomesTransactions() {
  const dispatch = useDispatch();
  //TODO: Test later
  const { pagingParams, filteringParams } = useSelector(
    (state) => state.incomesDataViewSettings
  );
  const { startDate, endDate } = filteringParams;
  const { pageNumber, pageSize } = pagingParams;

  const [curPageSize, setCurPageSize] = useState(pageSize);
  const [curViewPage, setCurViewNextPage] = useState(pageNumber);
  const [curViewDateRange, setCurViewDateRange] = useState([
    new Date(startDate),
    new Date(endDate),
  ]);
  const [curViewStartDate, curViewEndDate] = curViewDateRange;

  const handlePageChange = (event, data) => {
    setCurViewNextPage(data.activePage);
    dispatch(setPageNumber(data.activePage));
  };

  //TODO: convert date to string
  const handleDateRangeChange = (passedDateRangeArr) => {
    console.log(passedDateRangeArr);
    const objDateRan = { ...passedDateRangeArr };
    dispatch(setCurViewDateRange(objDateRan));
  };

  const data = [
    { name: "Group A", value: 400 },
    { name: "Group B", value: 300 },
    { name: "Group C", value: 300 },
    { name: "Group D", value: 200 },
  ];

  const COLORS = ["#0088FE", "#00C49F", "#FFBB28", "#FF8042"];

  const RADIAN = Math.PI / 180;
  const renderCustomizedLabel = ({
    cx,
    cy,
    midAngle,
    innerRadius,
    outerRadius,
    percent,
  }) => {
    const radius = innerRadius + (outerRadius - innerRadius) * 0.5;
    const x = cx + radius * Math.cos(-midAngle * RADIAN);
    const y = cy + radius * Math.sin(-midAngle * RADIAN);

    return (
      <text
        x={x}
        y={y}
        fill="white"
        textAnchor={x > cx ? "start" : "end"}
        dominantBaseline="central"
      >
        {`${(percent * 100).toFixed(0)}%`}
      </text>
    );
  };

  return (
    <Grid container>
      <GridRow
        verticalAlign="middle"
        centered
        columns={2}
        className="feature-content-section"
      >
        <GridColumn className="centered-chart">
          <ResponsiveContainer>
            <PieChart>
              <Pie
                data={data}
                cx="50%"
                cy="50%"
                innerRadius={"72%"}
                outerRadius={"98%"}
                fill="#8884d8"
                nameKey="asasasa"
                paddingAngle={5}
                dataKey="value"
              >
                {data.map((entry, index) => (
                  <Cell
                    key={`cell-${index}`}
                    fill={COLORS[index % COLORS.length]}
                  />
                ))}
              </Pie>
            </PieChart>
            <div className="income-amount absolute-centered">
              <h1>+2500$</h1>
            </div>
          </ResponsiveContainer>
        </GridColumn>
        <GridColumn width={5}>
          <List
            divided
            size="small"
            relaxed={{ relaxed: true }}
            className="vertical-scrollable-content"
          >
            <ListItem>
              <h4>
                <ListContent
                  floated="right"
                  className="income-amount relative-vertical-centered"
                >
                  255$
                </ListContent>
                <ListHeader>
                  <Label circular horizontal size="big" color="orange">
                    <Icon
                      fitted={true}
                      size="small"
                      name="money bill alternate outline"
                    />
                  </Label>
                  Salary
                </ListHeader>
              </h4>
            </ListItem>
            <ListItem>
              <h4>
                <ListContent
                  floated="right"
                  className="income-amount relative-vertical-centered"
                >
                  455$
                </ListContent>
                <ListHeader>
                  <Label circular horizontal size="big" color="blue">
                    <Icon
                      fitted={true}
                      size="small"
                      name="money bill alternate outline"
                    />
                  </Label>
                  Salary
                </ListHeader>
              </h4>
            </ListItem>
            <ListItem>
              <h4>
                <ListContent
                  floated="right"
                  className="income-amount relative-vertical-centered"
                >
                  455$
                </ListContent>
                <ListHeader>
                  <Label circular horizontal size="big" color="blue">
                    <Icon
                      fitted={true}
                      size="small"
                      name="money bill alternate outline"
                    />
                  </Label>
                  Salary
                </ListHeader>
              </h4>
            </ListItem>
            <ListItem>
              <h4>
                <ListContent
                  floated="right"
                  className="income-amount relative-vertical-centered"
                >
                  455$
                </ListContent>
                <ListHeader>
                  <Label circular horizontal size="big" color="blue">
                    <Icon
                      fitted={true}
                      size="small"
                      name="money bill alternate outline"
                    />
                  </Label>
                  Salary
                </ListHeader>
              </h4>
            </ListItem>
          </List>
        </GridColumn>
      </GridRow>
      <Divider hidden />
      <GridRow className="feature-content-section">
        <GridColumn>
        <Divider hidden />
          {/* //TODO change style for background color */}
          <div className="right-aligned">
    <DatePicker
      showIcon
      selectsRange={true}
      startDate={curViewStartDate}
      endDate={curViewEndDate}
      onChange={(selectedDateRange) => {
        setCurViewDateRange(selectedDateRange);
        let isNotNullDate = (dateValue) => dateValue != null;
        if (selectedDateRange.every(isNotNullDate))
          handleDateRangeChange(selectedDateRange);
      }}
      isClearable={true}
    />
  </div>
          <Divider hidden />
          {/* TODO add states of loading */}
          <IncomesItems />
          <Divider hidden />
          <Pagination
            style={{ background: "#8f48c7" }}
            onPageChange={handlePageChange}
            defaultActivePage={curViewPage}
            totalPages={pageSize}
          />
        </GridColumn>
      </GridRow>
    </Grid>
  );
}
