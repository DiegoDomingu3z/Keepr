<template>
  <div
    @click="setActive"
    class="img-container rounded selectable"
    :style="`background-image: Url(${keep.img})`"
  >
    <div class="d-flex justify-content-between mx-2 pt-1">
      <span
        ><h4>{{ keep.name }}</h4></span
      ><span @click="goToProfile"
        ><img class="profile-image" :src="keep.creator.picture" alt=""
      /></span>
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
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.img-container {
  background-position: center;
  background-size: cover;
  object-fit: cover;
  height: 50vh;
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
</style>