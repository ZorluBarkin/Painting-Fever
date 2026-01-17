extends TextureButton
@export_category("Button Colors")
@export var hover_color: Color = Color.WHITE
@export var pressed_color: Color = Color.DARK_ORANGE
@export var normal_color: Color = Color.LIGHT_GRAY

@export_category("Label colors")
@export var label_light_color: Color = Color.WHITE
@export var label_dark_color: Color = Color.BLACK

var _label: Label = null

func _ready():
	normal_color = self_modulate
	
	# get the label if there is one
	_get_label()
	
	mouse_entered.connect(_on_hover)
	mouse_exited.connect(_on_normal)
	button_down.connect(_on_pressed)
	button_up.connect(_on_hover)

func _get_label():
	for child in find_children("*", "Label"):
		if child is Label:
			_label = child
			_update_label_color()
			break;

func _update_label_color():
	if _label: # null check
		if (self_modulate.get_luminance() > 0.6):
			_label.self_modulate = label_dark_color
		else:
			_label.self_modulate = label_light_color

func _on_hover():
	self_modulate = hover_color
	_update_label_color()

func _on_normal():
	self_modulate = normal_color
	_update_label_color()

func _on_pressed():
	self_modulate = pressed_color
	_update_label_color()
