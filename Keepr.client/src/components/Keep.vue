<template>
  <div @click="setActive" class="img-container rounded selectable">
    <img title="See Details" class="img-fluid rounded" :src="keep.img" alt="" />

    <div class="d-flex justify-content-between pt-1">
      <div class="keep-info text-white fx-bolder">
        <h3>{{ keep.name }}</h3>
      </div>
      <div class="keep-info-2" @click.stop="goToProfile">
        <img
          title="Go To Profile"
          class="profile-image"
          :src="keep.creator.picture"
          alt=""
        />
      </div>
    </div>
  </div>
  <Modal id="active-keep">
    <template #body><ActiveKeepModal /> </template>
  </Modal>
</template>


<script>
import { Modal } from 'bootstrap'
import { useRouter } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { profilesService } from '../services/ProfilesService'
export default {
  props: { keep: { type: Object, required: true } },
  setup(props) {
    const router = useRouter()
    return {
      goToProfile() {
        router.push({ name: "Profile", params: { id: props.keep.creator.id } })
        console.log(props.keep.creator.id);

      },
      async setActive() {
        try {
          await keepsService.setActive(props.keep.id)
          Modal.getOrCreateInstance(document.getElementById("active-keep")).show();
        } catch (error) {
          logger.log(error)
          Pop.toast(error.message)
        }
      },
    }
  }
}
</script>


<style lang="scss" scoped>
.img-container {
  position: relative;
}
.img-container:hover {
  transform: scale(1.02);
  transition: 350ms;
}

.profile-image {
  all: unset;
  height: 40px;
  border-radius: 50%;
}

.keep-info {
  position: absolute;
  z-index: 100;
  left: 1em;
  bottom: 0.3em;
}

.keep-info-2 {
  position: absolute;
  z-index: 100;
  right: 1em;
  bottom: 0.3em;
}

h3 {
  text-shadow: 2px 2px black;
}
</style>