import React from 'react';
import { Button, Grid } from 'semantic-ui-react';
import { useStore } from '../../stores/store';
import { observer } from 'mobx-react-lite';
import ProducerList from './ProducerList';
import ProducerForm from '../form/ProducerForm';
import { ProducerDetail } from './ProducerDetail';

const Producers = () => {
  const { producerStore } = useStore();
  const { selectedProducer, editMode, openForm } = producerStore;
  return (
    <Grid>
      <Grid.Row style={{ justifyContent: 'right' }}>
        <Button onClick={() => openForm()} positive content="Create Producer" />
      </Grid.Row>
      <Grid.Row>
        <Grid.Column width="10">
          <ProducerList />
        </Grid.Column>
        <Grid.Column width="6">
          {selectedProducer && !editMode && <ProducerDetail />}
          {editMode && <ProducerForm />}
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default observer(Producers);
